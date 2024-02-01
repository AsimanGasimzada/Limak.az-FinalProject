using AutoMapper;
using Limak.Application.DTOs.ShopDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class ShopAutoMapper:Profile
{
    public ShopAutoMapper()
    {
        CreateMap<Shop, ShopGetDto>().ReverseMap();
        CreateMap<Shop, ShopPutDto>().ReverseMap();
        CreateMap<Shop, ShopPostDto>().ReverseMap();
    }
}
