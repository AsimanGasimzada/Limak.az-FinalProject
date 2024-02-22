using AutoMapper;
using Limak.Application.DTOs.StatusDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class StatusAutoMapper:Profile
{
    public StatusAutoMapper()
    {
        CreateMap<Status, StatusGetDto>().ReverseMap();
        CreateMap<Status, StatusPostDto>().ReverseMap();
        CreateMap<Status, StatusPutDto>().ReverseMap();
        CreateMap<Status, StatusRelationDto>().ReverseMap();
    }
}
