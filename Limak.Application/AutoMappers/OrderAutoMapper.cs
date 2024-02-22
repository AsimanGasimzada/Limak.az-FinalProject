using AutoMapper;
using Limak.Application.DTOs.OrderDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class OrderAutoMapper:Profile
{
    public OrderAutoMapper()
    {
        CreateMap<Order, OrderPostDto>().ReverseMap();
        CreateMap<Order, OrderPutDto>().ReverseMap();
        CreateMap<Order, OrderAdminPutDto>().ReverseMap();
        CreateMap<Order, OrderGetDto>().ReverseMap();
        CreateMap<Order, OrderRelationDto>().ReverseMap();
    }
}
