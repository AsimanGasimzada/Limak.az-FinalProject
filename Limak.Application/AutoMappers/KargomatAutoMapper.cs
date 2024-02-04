using AutoMapper;
using Limak.Application.DTOs.KargomatDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class KargomatAutoMapper:Profile
{
    public KargomatAutoMapper()
    {
        CreateMap<Kargomat, KargomatGetDto>().ReverseMap();
        CreateMap<Kargomat, KargomatPostDto>().ReverseMap();
        CreateMap<Kargomat, KargomatPutDto>().ReverseMap();
    }
}
