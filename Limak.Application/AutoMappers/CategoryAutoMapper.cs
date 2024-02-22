using AutoMapper;
using Limak.Application.DTOs.CategoryDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class CategoryAutoMapper : Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryPostDto>().ReverseMap();
        CreateMap<CategoryGetDto, Category>().ReverseMap().
            ForMember(dest => dest.Shops,
            opt => opt.MapFrom(src => src.ShopCategories.Select(sc => sc.Shop)));
        
        CreateMap<Category, CategoryPutDto>().ReverseMap();
        CreateMap<Category, CategoryRelationDto>().ReverseMap();
    }
}
