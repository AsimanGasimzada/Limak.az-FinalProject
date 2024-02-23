using AutoMapper;
using Limak.Application.DTOs.BrandDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class BrandAutoMapper:Profile
{
    public BrandAutoMapper()
    {
        CreateMap<Brand, BrandGetDto>().ReverseMap();
        CreateMap<Brand, BrandPutDto>().ReverseMap();
        CreateMap<Brand, BrandPostDto>().ReverseMap();
    }
}
