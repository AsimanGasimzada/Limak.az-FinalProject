using AutoMapper;
using Limak.Application.Abstractions.Helpers;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.OrderDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Limak.Persistence.Implementations.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly UserManager<AppUser> _userManager;
    private readonly ICountryService _countryService;
    private readonly IWarehouseService _warehouseService;
    private readonly IStatusService _statusService;
    private readonly ITransactionService _transactionService;
    private readonly IEmailHelper _emailHelper;
    private readonly INotificationService _notificationService;
    public OrderService(IOrderRepository repository, IMapper mapper, IHttpContextAccessor contextAccessor, UserManager<AppUser> userManager, ICountryService countryService, IWarehouseService warehouseService, IStatusService statusService, ITransactionService transactionService, IEmailHelper emailHelper, INotificationService noNotificationService)
    {
        _repository = repository;
        _mapper = mapper;
        _contextAccessor = contextAccessor;
        _userManager = userManager;
        _countryService = countryService;
        _warehouseService = warehouseService;
        _statusService = statusService;
        _transactionService = transactionService;
        _emailHelper = emailHelper;
        _notificationService = noNotificationService;
    }

    public async Task<ResultDto> CreateAsync(OrderPostDto dto)
    {
        var user = await GetUser();
        var isExistCountry = await _countryService.IsExist(dto.CountryId);
        if (!isExistCountry)
            throw new NotFoundException($"{dto.CountryId}-This Country is not found!");

        var isExistWarehouse = await _warehouseService.IsExist(dto.WarehouseId);
        if (!isExistWarehouse)
            throw new NotFoundException($"{dto.WarehouseId}-This Warehouse is not found!");

        var order = _mapper.Map<Order>(dto);
        order.TotalPrice = (decimal)(order.Price * order.Count) + order.LocalCargoPrice;
        order.TotalPrice = order.TotalPrice * 1.05m;
        order.AppUser = user;
        order.StatusId = (await _statusService.GetByNameAsync(StatusNames.NotOrdered)).Id;

        await _repository.CreateAsync(order);
        await _repository.SaveAsync();

        return new("Order is successfully created");

    }


    public async Task<ResultDto> DeleteAsync(int id)
    {
        Order order = await _getOrder(id);
        if (order.Status.Name != StatusNames.NotOrdered)
            throw new CannotDeleteException($"{id}-this order cannot be deleted");

        var currentUser = await GetUser();
        var currentUserRole = (await _userManager.GetRolesAsync(currentUser)).FirstOrDefault();

        if (order.AppUser != currentUser && currentUserRole != IdentityRoles.Admin.ToString())
            throw new CannotDeleteException($"{id}-a Order can only be deleted by its owner and admin");

        _repository.HardDelete(order);
        await _repository.SaveAsync();

        return new($"{id}-Order is successfully deleted");
    }



    public async Task<OrderGetDto> GetByIdAsync(int id)
    {
        var order = await _getOrder(id);
        var currentUser = await GetUser();
        var currentUserRole = (await _userManager.GetRolesAsync(currentUser)).FirstOrDefault();

        if (order.AppUser != currentUser && currentUserRole != IdentityRoles.Admin.ToString())
            throw new UnAuthorizedException($"{id}-a Order can only be get by its owner and admin");

        var dto = _mapper.Map<OrderGetDto>(order);
        return dto;

    }

    public async Task<List<OrderGetDto>> GetNotPaymentOrders()
    {
        var user = await GetUser();
        var orders = await _repository.GetFiltered(x => x.AppUser == user && x.Status.Name == StatusNames.NotOrdered, false, "AppUser", "Status", "Country").ToListAsync();
        if (orders.Count is 0)
            throw new NotFoundException("Order is not found!");

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return dtos;
    }

    public async Task<List<OrderGetDto>> GetUserAllOrders()
    {
        var user = await GetUser();
        var orders = await _repository.GetFiltered(x => x.AppUser == user, false, "AppUser", "Status", "Country").ToListAsync();
        if (orders.Count is 0)
            throw new NotFoundException("Order is not found!");

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return dtos;
    }

    public async Task<bool> IsExist(int id)
    {
        var order = await _repository.GetSingleAsync(x => x.Id == id, false, "AppUser");

        if (order is null)
            return false;

        var currentUser = await GetUser();
        return (order.AppUser == currentUser);

    }
    public async Task<ResultDto> PayOrders(List<int> orderIds)
    {
        var user = await GetUser();
        List<Order> orders = new List<Order>();
        orderIds.ForEach(x => orders.Add(_getOrder(x).Result));
        var country = orders.FirstOrDefault()?.Country;

        orders.ForEach(x => { if (x.AppUser != user || x.Country != country || x.Status.Name != StatusNames.NotOrdered) throw new InvalidInputException($"{x.Id}-This Order not found"); });

        decimal totalPrice = 0;
        orders.ForEach(x => totalPrice += x.TotalPrice);

        if (country?.Name == CountryNames.Turkey)
            await _transactionService.PaymentByTRYBalance(new() { Amount = totalPrice });

        if (country?.Name == CountryNames.America)
            await _transactionService.PaymentByUSDBalance(new() { Amount = totalPrice });

        var paidStatus = await _statusService.GetByNameAsync(StatusNames.Paid);

        orders.ForEach(x =>
        {
            x.StatusId = paidStatus.Id;
            _repository.Update(x);
        });

        await _repository.SaveAsync();

        return new($"Payment is successfully Total-{totalPrice}");
    }

    public async Task<ResultDto> UpdateAsync(OrderPutDto dto)
    {
        var order = await _getOrder(dto.Id);
        if (order.Status.Name != StatusNames.NotOrdered && order.Status.Name != StatusNames.Paid)
            throw new CannotDeleteException($"{dto.Id}-this order cannot be updated");

        var currentUser = await GetUser();


        if (order.AppUser != currentUser)
            throw new UnAuthorizedException($"{dto.Id}-a Order can only be updated by it owner");

        order = _mapper.Map(dto, order);

        _repository.Update(order);
        await _repository.SaveAsync();

        return new($"{order.Id}-Order is successfully updated");
    }



    // Admin methods


    public async Task<List<OrderGetDto>> GetAllAsync()
    {
        var orders = await _repository.GetAll(false, "AppUser", "Status", "Delivery", "Warehouse", "Kargomat", "Country").ToListAsync();

        if (orders.Count is 0)
            throw new NotFoundException("Order is not found!");

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return dtos;
    }

    public async Task<ResultDto> OrderCancelAsync(OrderCancelDto dto)
    {
        var order = await _getOrder(dto.Id);
        order.IsCancel = true;
        order.CancellationNotes = dto.CancellationNotes;
        decimal amount = (order.Price * order.Count) + order.LocalCargoPrice;
        if (order.Country.Name == CountryNames.Turkey)
            await _transactionService.IncreaseTRYBalanceAdmin(new() { AppUserId = order.AppUserId, Amount = amount });

        if (order.Country.Name == CountryNames.America)
            await _transactionService.IncreaseUSDBalanceAdmin(new() { AppUserId = order.AppUserId, Amount = amount });

        _repository.Update(order);
        await _repository.SaveAsync();

        return new($"{order.Id}-Order is canceled");

    }


    public async Task<ResultDto> UpdateOrderByAdminAsync(OrderAdminPutDto dto)
    {
        var existed = await _getOrder(dto.Id);

        existed = _mapper.Map(dto, existed);

        existed.TotalPrice = (decimal)(existed.Price * existed.Count) + existed.LocalCargoPrice + existed.AdditionFees;
        existed.TotalPrice = existed.TotalPrice * 1.05m;
        existed.StatusId = (await _statusService.GetByNameAsync(StatusNames.Ordered)).Id;


        _repository.Update(existed);
        await _repository.SaveAsync();

        return new($"{existed.Id}-Order successfully edited");
    }


    public async Task<ResultDto> ChangeOrderStatusAsync(OrderChangeStatusDto dto)
    {
        var order = await _getOrder(dto.Id);

        var Status = await _statusService.GetByIdAsync(dto.StatusId);
        if (Status is null)
            throw new InvalidInputException($"{dto.StatusId}-this status is not found");

        order.StatusId = dto.StatusId;


        
        await _notificationService.CreateAsync(new() { AppUserId = order.AppUserId, Subject = "Sifarişinizin statusu dəyişdi", Title = $"{order.Id}-nömrəli sifarişiniz {Status.Name}-mərhələsinə keçdi" });



        _repository.Update(order);
        await _repository.SaveAsync();

        return new($"{order.Id}-Order is successfully updated");
    }

    private async Task<AppUser> GetUser()
    {
        var id = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new UnAuthorizedException();
        return user;
    }
    private async Task<Order> _getOrder(int id)
    {
        var order = await _repository.GetSingleAsync(x => x.Id == id, false, "Status", "AppUser", "Country");
        if (order is null)
            throw new NotFoundException($"{id}-This order is not found");
        return order;
    }

    public async Task<ResultDto> SetDelivery(List<int> orderIds, Delivery Delivery)
    {
        foreach (var orderId in orderIds)
        {
            var order = await _getOrder(orderId);
            order.Delivery = Delivery;
            _repository.Update(order);
        }
        return new("Delivery is successfully set");
    }

    public async Task<ResultDto> CancelDelivery(Delivery delivery)
    {
        var status =await _statusService.GetByNameAsync(StatusNames.LocalWarehouse);
        foreach (var order in delivery.Orders)
        {
            order.StatusId = status.Id;
            order.DeliveryId = null;
            _repository.Update(order);
        }
        return new("Delivery is successfully canceled");
    }
}
