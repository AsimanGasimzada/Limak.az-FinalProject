using AutoMapper;
using Limak.Application.DTOs.SettingDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class SettingAutoMapper:Profile
{
    public SettingAutoMapper()
    {
        CreateMap<Setting, SettingGetDto>().ReverseMap();
        CreateMap<Setting, SettingPostDto>().ReverseMap();
        CreateMap<Setting, SettingPutDto>().ReverseMap();
    }
}
