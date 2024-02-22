using AutoMapper;
using Limak.Application.Abstractions.Helpers;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.OrderDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.Validators.AuthValidators;
using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICountryService _countryService;
    private readonly IAuthService _authService;
    private readonly IWarehouseService _warehouseService;
    private readonly IStatusService _statusService;
    private readonly ITransactionService _transactionService;
    private readonly IEmailHelper _emailHelper;
    private readonly INotificationService _notificationService;
    private readonly IKargomatService _kargomatService;
    private readonly ITariffService _tariffService;
    public OrderService(IOrderRepository repository, IMapper mapper, ICountryService countryService, IWarehouseService warehouseService, IStatusService statusService, ITransactionService transactionService, IEmailHelper emailHelper, INotificationService noNotificationService, IAuthService authService, IKargomatService kargomatService, ITariffService tariffService)
    {
        _repository = repository;
        _mapper = mapper;
        _countryService = countryService;
        _warehouseService = warehouseService;
        _statusService = statusService;
        _transactionService = transactionService;
        _emailHelper = emailHelper;
        _notificationService = noNotificationService;
        _authService = authService;
        _kargomatService = kargomatService;
        _tariffService = tariffService;
    }

    public async Task<ResultDto> CreateAsync(OrderPostDto dto)
    {
        var user = await _authService.GetCurrentUserAsync();
        var isExistCountry = await _countryService.IsExist(dto.CountryId);
        if (!isExistCountry)
            throw new NotFoundException($"{dto.CountryId}-This Country is not found!");

        var isExistWarehouse = await _warehouseService.IsExist(dto.WarehouseId);
        if (!isExistWarehouse)
            throw new NotFoundException($"{dto.WarehouseId}-This Warehouse is not found!");

        var order = _mapper.Map<Order>(dto);
        order.OrderTotalPrice = (order.Price * order.Count) + order.LocalCargoPrice;

        var turkey = await _countryService.GetByNameAsync(CountryNames.Turkey);
        var america = await _countryService.GetByNameAsync(CountryNames.America);

        if (order.CountryId == turkey.Id)
            order.TotalPrice = order.TotalPrice * 1.05m;

        if (order.CountryId == america.Id)
            order.TotalPrice = order.TotalPrice * 1.07m;



        order.AppUserId = user.Id;
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

        var currentUser = await _authService.GetCurrentUserAsync();
        var currentUserRole = await _authService.GetUserRoleAsync(currentUser.Id);

        if (order.AppUserId != currentUser.Id && currentUserRole != IdentityRoles.Admin.ToString())
            throw new CannotDeleteException($"{id}-a Order can only be deleted by its owner and admin");

        _repository.HardDelete(order);
        await _repository.SaveAsync();

        return new($"{id}-Order is successfully deleted");
    }
    public async Task<OrderGetDto> GetByIdAsync(int id)
    {
        var order = await _getOrder(id);
        var currentUser = await _authService.GetCurrentUserAsync();
        var currentUserRole = await _authService.GetUserRoleAsync(currentUser.Id);

        if (order.AppUserId != currentUser.Id && currentUserRole != IdentityRoles.Admin.ToString())
            throw new UnAuthorizedException($"{id}-a Order can only be get by its owner and admin");

        var dto = _mapper.Map<OrderGetDto>(order);
        return dto;

    }
    public async Task<List<OrderGetDto>> GetNotPaymentOrders()
    {
        var user = await _authService.GetCurrentUserAsync();
        var orders = await _repository.GetFiltered(x => x.AppUserId == user.Id && x.Status.Name == StatusNames.NotOrdered, false, "AppUser", "Status", "Country", "Warehouse", "Kargomat", "Delivery", "Shop").ToListAsync();
        if (orders.Count is 0)
            throw new NotFoundException("Order is not found!");

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return dtos;
    }
    public async Task<List<OrderGetDto>> GetUserAllOrders()
    {
        var user = await _authService.GetCurrentUserAsync();
        var orders = await _repository.GetFiltered(x => x.AppUserId == user.Id, false, "AppUser", "Status", "Country", "Warehouse", "Kargomat", "Delivery", "Shop").ToListAsync();
        if (orders.Count is 0)
            throw new NotFoundException("Order is not found!");

        var dtos = _mapper.Map<List<OrderGetDto>>(orders);
        return dtos;
    }
    public async Task<bool> IsExistAsync(int id)
    {
        var order = await _repository.GetSingleAsync(x => x.Id == id, false, "AppUser");

        if (order is null)
            return false;

        var currentUser = await _authService.GetCurrentUserAsync();
        return (order.AppUserId == currentUser.Id);

    }
    public async Task<ResultDto> PayOrders(List<int> orderIds)
    {
        var user = await _authService.GetCurrentUserAsync();
        List<Order> orders = new List<Order>();
        orderIds.ForEach(x => orders.Add(_getOrder(x).Result));
        var country = orders.FirstOrDefault()?.Country;

        orders.ForEach(x => { if (x.AppUserId != user.Id || x.Country != country || x.Status.Name != StatusNames.NotOrdered) throw new InvalidInputException($"{x.Id}-This Order not found"); });

        decimal totalPrice = 0;
        orders.ForEach(x => totalPrice += x.OrderTotalPrice);

        if (country?.Name == CountryNames.Turkey)
            await _transactionService.PaymentByTRYBalance(new() { Amount = totalPrice, OrderIds = orderIds });

        if (country?.Name == CountryNames.America)
            await _transactionService.PaymentByUSDBalance(new() { Amount = totalPrice, OrderIds = orderIds });

        var paidStatus = await _statusService.GetByNameAsync(StatusNames.Paid);

        orders.ForEach(x =>
        {
            x.StatusId = paidStatus.Id;
            x.OrderPaymentStatus = true;
            _repository.Update(x);
        });

        await _repository.SaveAsync();

        return new($"Payment is successfully Total-{totalPrice}");
    }
    public async Task<ResultDto> PayFullOrder(int orderId)
    {
        var user = await _authService.GetCurrentUserAsync();
        var order = await _getOrder(orderId);


        if (order.AppUserId != user.Id)
            throw new NotFoundException($"{orderId}-Order is not found");

        var status1 = await _statusService.GetByNameAsync(StatusNames.ForeignWarehouse);
        var status2 = await _statusService.GetByNameAsync(StatusNames.Customs);
        var status3 = await _statusService.GetByNameAsync(StatusNames.OnTheWay);
        var status4 = await _statusService.GetByNameAsync(StatusNames.LocalWarehouse);
        var status5 = await _statusService.GetByNameAsync(StatusNames.Kargomat);
        var status6 = await _statusService.GetByNameAsync(StatusNames.Delivery);


        if (order.StatusId != status1.Id && order.StatusId != status2.Id && order.StatusId != status3.Id && order.StatusId != status4.Id && order.StatusId != status5.Id && order.StatusId != status6.Id)
            throw new NotFoundException($"{orderId}-Order is not found");

        if (order.OrderPaymentStatus && order.CargoPaymentStatus)
            throw new NotFoundException($"{orderId}-Order is not found");

        if (order.Country.Name == CountryNames.Turkey)
        {
            await _transactionService.PaymentByAZNBalance(new() { Amount = order.TotalPrice, OrderIds = new() { orderId } });

            if (!order.OrderPaymentStatus)
                await _transactionService.PaymentByTRYBalance(new() { Amount = order.OrderTotalPrice, OrderIds = new() { orderId } });


        }

        if (order.Country.Name == CountryNames.America)
        {
            await _transactionService.PaymentByAZNBalance(new() { Amount = order.TotalPrice, OrderIds = new() { orderId } });

            if (!order.OrderPaymentStatus)
                await _transactionService.PaymentByUSDBalance(new() { Amount = order.OrderTotalPrice, OrderIds = new() { orderId } });
        }
        order.CargoPaymentStatus = true;
        order.OrderPaymentStatus = true;

        _repository.Update(order);
        await _repository.SaveAsync();

        return new($"{order.Id}-order is payment is done");
    }
    public async Task<ResultDto> UpdateAsync(OrderPutDto dto)
    {
        var order = await _getOrder(dto.Id);
        if (order.Status.Name != StatusNames.NotOrdered && order.Status.Name != StatusNames.Paid)
            throw new CannotDeleteException($"{dto.Id}-this order cannot be updated");

        var currentUser = await _authService.GetCurrentUserAsync();


        if (order.AppUserId != currentUser.Id)
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

        var tariff = await _tariffService.GetTariffByWeight(dto.Weight, existed.CountryId);
        existed.CargoPrice = tariff.Price;
        existed.TotalPrice = existed.AdditionFees + tariff.Price;
        existed.StatusId = (await _statusService.GetByNameAsync(StatusNames.ForeignWarehouse)).Id;



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
        var status = await _statusService.GetByNameAsync(StatusNames.LocalWarehouse);
        foreach (var order in delivery.Orders)
        {
            order.StatusId = status.Id;
            order.DeliveryId = null;
            _repository.Update(order);
        }
        return new("Delivery is successfully canceled");
    }
    public async Task<ResultDto> SetKargomatAsync(OrderSetKargomatDto dto)
    {
        var isExistKargomat = await _kargomatService.IsExistAsync(dto.KargomatId);

        if (!isExistKargomat)
            throw new($"{dto.KargomatId}-this Kargomat is not found");

        var order = await _getOrder(dto.OrderId);

        order.KargomatId = dto.KargomatId;

        _repository.Update(order);
        await _repository.SaveAsync();

        return new("Kargomat is successfully set");
    }
    public async Task<ResultDto> CancelKargomatAsync(int orderId)
    {
        var order = await _getOrder(orderId);

        var status = await _statusService.GetByNameAsync(StatusNames.Kargomat);

        if (order.StatusId == status.Id)
            throw new("Canceled is not successfully");


        order.KargomatId = null;

        _repository.Update(order);
        await _repository.SaveAsync();

        return new("Kargomat is successfully created");
    }
    public async Task<ResultDto> CreateByAdminAsync(OrderAdminPostDto dto)
    {

        var user = await _authService.GetUserByUsernameAsync(dto.UserName);

        var isExistCountry = await _countryService.IsExist(dto.CountryId);
        if (!isExistCountry)
            throw new NotFoundException($"{dto.CountryId}-This Country is not found!");

        var isExistWarehouse = await _warehouseService.IsExist(dto.WarehouseId);
        if (!isExistWarehouse)
            throw new NotFoundException($"{dto.WarehouseId}-This Warehouse is not found!");

        var order = _mapper.Map<Order>(dto);

        var tariff = await _tariffService.GetTariffByWeight(dto.Weight, dto.CountryId);
        var status = await _statusService.GetByNameAsync(StatusNames.ForeignWarehouse);



        order.CargoPrice = tariff.Price;
        order.AppUserId = user.Id;
        order.OrderTotalPrice = dto.LocalCargoPrice + (dto.Price * order.Count);

        var turkey = await _countryService.GetByNameAsync(CountryNames.Turkey);
        var america = await _countryService.GetByNameAsync(CountryNames.America);

        if (order.CountryId == turkey.Id)
            order.OrderTotalPrice = order.OrderTotalPrice * 1.05m;

        if (order.CountryId == america.Id)
            order.OrderTotalPrice = order.OrderTotalPrice * 1.07m;

        order.TotalPrice = tariff.Price;
        order.StatusId = status.Id;



        await _repository.CreateAsync(order);
        await _repository.SaveAsync();

        await _notificationService.CreateAsync(new() { AppUserId = order.AppUserId, Subject = "Xarici anbara bağlamanız daxil olub", Title = $" sifarişiniz {status.Name}-mərhələsinə keçdi,zəhmət olmasa SmartCustomsda bəyan edin." });


        return new("Order is successfully created");

    }


    private async Task<Order> _getOrder(int id)
    {
        var order = await _repository.GetSingleAsync(x => x.Id == id, false, "Status", "AppUser", "Country");
        if (order is null)
            throw new NotFoundException($"{id}-This order is not found");
        return order;
    }
}
