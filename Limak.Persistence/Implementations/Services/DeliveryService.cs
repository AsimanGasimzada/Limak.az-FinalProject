using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.DeliveryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _repository;
    private readonly IOrderService _orderService;
    private readonly IStatusService _statusService;
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IDeliveryAreaService _deliveryAreaService;

    public DeliveryService(IDeliveryRepository repository, IOrderService orderService, IStatusService statusService, IMapper mapper, IAuthService authService, IDeliveryAreaService deliveryAreaService)
    {
        _repository = repository;
        _orderService = orderService;
        _statusService = statusService;
        _mapper = mapper;
        _authService = authService;
        _deliveryAreaService = deliveryAreaService;
    }

    public async Task<ResultDto> CancelDeliveryAsync(int id)
    {

        var delivery = await _repository.GetSingleAsync(x => x.Id == id, false, "Orders");
        if (delivery is null)
            throw new NotFoundException($"{id}-Delivery is not found!");

        var status = await _statusService.GetByNameAsync(StatusNames.LocalWarehouse);

        await _orderService.CancelDelivery(delivery);
        delivery.IsCancel = true;
        
        _repository.Update(delivery);
        await _repository.SaveAsync();

        return new("Delivery is successfully canceled");
    }

    public async Task<ResultDto> CreateDeliveryAsync(DeliveryPostDto dto)
    {
        var user = await _authService.GetCurrentUserAsync();

        var deliveryArea = await _deliveryAreaService.GetByIdAsync(dto.DeliveryAreaId);
        
        var status = await _statusService.GetByNameAsync(StatusNames.LocalWarehouse);



        foreach (var x in dto.OrderIds)
        {
            var order = await _orderService.GetByIdAsync(x);
            if (order.AppUserId != user.Id)
                throw new UnAuthorizedException();
            if (order.WarehouseId != deliveryArea.WarehouseId)
                throw new NotFoundException($"{dto.DeliveryAreaId}-this delivery area is not found");
            if (order.StatusId != status.Id)
                throw new NotFoundException($"{x}-this Order is not found!");
        };


        var delivery = _mapper.Map<Delivery>(dto);
        delivery.AppUserId = user.Id;

        await _repository.CreateAsync(delivery);
        await _orderService.SetDelivery(dto.OrderIds, delivery);
        await _repository.SaveAsync();


        return new("Delivery successfully created");
    }

    public async Task<List<DeliveryGetDto>> GetAllAdminAsync(int page = 1)
    {
        if (page < 1) page = 1;
        var query = _repository.GetAll(false, "AppUser", "Orders", "DeliveryArea");
        var deliveries = await _repository.Paginate(query, 10, page).ToListAsync();

        if (deliveries.Count is 0)
            throw new NotFoundException("Delivery is not found");

        var dtos = _mapper.Map<List<DeliveryGetDto>>(deliveries);

        return dtos;
    }

    public async Task<List<DeliveryGetDto>> GetAllAsync()
    {
        var user = await _authService.GetCurrentUserAsync();

        var deliveries = await _repository.GetFiltered(x => x.AppUserId == user.Id, false, "AppUser", "Orders", "DeliveryArea").ToListAsync();
        if (deliveries.Count is 0)
            throw new NotFoundException("Delivery is not found");

        var dtos = _mapper.Map<List<DeliveryGetDto>>(deliveries);

        return dtos;
    }

    public async Task<DeliveryGetDto> GetByIdAsync(int id)
    {
        var delivery = await _repository.GetSingleAsync(x => x.Id == id, false, "AppUser", "DeliveryArea", "Orders");

        if (delivery is null)
            throw new NotFoundException($"{id}-Delivery is not found");

        var dto=_mapper.Map<DeliveryGetDto>(delivery);  

        return dto;
    }
}
