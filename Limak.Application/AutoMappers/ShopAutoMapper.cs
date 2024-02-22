using AutoMapper;
using Limak.Application.DTOs.ShopDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class ShopAutoMapper:Profile
{
    public ShopAutoMapper()
    {
        CreateMap<ShopGetDto, Shop>().ReverseMap()
            .ForMember(dest => dest.Categories,
            opt => opt.MapFrom(src => src.ShopCategories.Select(sc => sc.Category))); ;
        CreateMap<Shop, ShopPutDto>().ReverseMap();
        CreateMap<Shop, ShopPostDto>().ReverseMap();
        CreateMap<Shop, ShopRelationDto>().ReverseMap();
    }
}
