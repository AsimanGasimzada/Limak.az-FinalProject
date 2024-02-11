using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.DeliveryDTOs;
using Limak.Application.DTOs.OrderDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Limak.Persistence.Implementations.Services;

public class DeliveryService : IDeliveryService
{
    private readonly IDeliveryRepository _repository;
    private readonly IOrderService _orderService;
    private readonly IStatusService _statusService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;

    public DeliveryService(IDeliveryRepository repository, IOrderService orderService, IStatusService statusService, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        _repository = repository;
        _orderService = orderService;
        _statusService = statusService;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
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
        var id = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new UnAuthorizedException();

        var status = await _statusService.GetByNameAsync(StatusNames.LocalWarehouse);
        dto.OrderIds.ForEach(async x =>
        {
            var order = await _orderService.GetByIdAsync(x);
            if (order.AppUserId != user.Id)
                throw new UnAuthorizedException();
            if (order.StatusId != status.Id)
                throw new NotFoundException("Order is not found!");
        });


        var delivery = _mapper.Map<Delivery>(dto);


        await _repository.CreateAsync(delivery);
        await _orderService.SetDelivery(dto.OrderIds, delivery);
        await _repository.SaveAsync();


        return new("Delivery successfully created");
    }
}
