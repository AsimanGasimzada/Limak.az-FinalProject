using AutoMapper;
using Limak.Application.DTOs.DeliveryDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class DeliveryAutoMapper:Profile
{
    public DeliveryAutoMapper()
    {
        CreateMap<Delivery, DeliveryPostDto>().ReverseMap();
        CreateMap<Delivery, DeliveryGetDto>().ReverseMap();
        CreateMap<Delivery, DeliveryRelationDto>().ReverseMap();
    }
}
