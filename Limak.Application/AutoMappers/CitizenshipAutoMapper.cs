using AutoMapper;
using Limak.Application.DTOs.CitizenshipDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class CitizenshipAutoMapper:Profile
{
    public CitizenshipAutoMapper()
    {
        CreateMap<Citizenship, CitizenshipGetDto>().ReverseMap();
        CreateMap<Citizenship, CitizenshipPostDto>().ReverseMap();
        CreateMap<Citizenship, CitizenshipPutDto>().ReverseMap();
    }
}
