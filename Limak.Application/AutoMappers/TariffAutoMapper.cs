using AutoMapper;
using Limak.Application.DTOs.TariffDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class TariffAutoMapper : Profile
{
    public TariffAutoMapper()
    {
        CreateMap<Tariff, TariffGetDto>().ReverseMap();
        CreateMap<Tariff, TariffPutDto>().ReverseMap();
        CreateMap<Tariff, TariffPostDto>().ReverseMap();
    }
}
