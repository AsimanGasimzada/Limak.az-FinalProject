using AutoMapper;
using Limak.Application.DTOs.GenderDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;


public class GenderAutoMapper:Profile
{
    public GenderAutoMapper()
    {
        CreateMap<Gender, GenderGetDto>().ReverseMap();
        CreateMap<Gender, GenderPostDto>().ReverseMap();
        CreateMap<Gender, GenderPutDto>().ReverseMap();
    }
}
