using AutoMapper;
using Limak.Application.DTOs.CategoryDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class CategoryAutoMapper:Profile
{
    public CategoryAutoMapper()
    {
        CreateMap<Category, CategoryPostDto>().ReverseMap();
        CreateMap<Category, CategoryGetDto>().ReverseMap();
        CreateMap<Category, CategoryPutDto>().ReverseMap();
    }
}
