using AutoMapper;
using Limak.Application.DTOs.DeliveryAreaDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class DeliveryAreaAutoMapper:Profile
{
    public DeliveryAreaAutoMapper()
    {
        CreateMap<DeliveryArea, DeliveryAreaGetDto>().ReverseMap();
        CreateMap<DeliveryArea, DeliveryAreaPostDto>().ReverseMap();
        CreateMap<DeliveryArea, DeliveryAreaPutDto>().ReverseMap();
    }
}
