using AutoMapper;
using Limak.Application.DTOs.AuthDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class AuthAutoMapper : Profile
{
    public AuthAutoMapper()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();
        CreateMap<AppUser, AppUserGetDto>().ReverseMap();
        CreateMap<AppUser, AppUserAccountDataPutDto>().ForMember(dest => dest.Email, opt => opt.Ignore()).ReverseMap();
        CreateMap<AppUser, AppUserPersonalDataPutDto>().ReverseMap();
        CreateMap<AppUser, AppUserRelationDto>().ReverseMap();
    }
}
