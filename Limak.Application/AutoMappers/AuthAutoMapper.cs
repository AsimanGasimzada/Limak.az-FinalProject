using AutoMapper;
using Limak.Application.DTOs.AuthDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class AuthAutoMapper:Profile
{
    public AuthAutoMapper()
    {
        CreateMap<AppUser, RegisterDto>().ReverseMap();

    }
}
