using AutoMapper;
using Limak.Application.DTOs.CountryDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class CountryAutoMapper : Profile
{
    public CountryAutoMapper()
    {
        CreateMap<Country, CountryGetDto>().ReverseMap();
        CreateMap<Country, CountryPutDto>().ReverseMap();
        CreateMap<Country, CountryPostDto>().ReverseMap();
    }
}
