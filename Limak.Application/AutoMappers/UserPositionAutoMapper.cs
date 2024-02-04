using AutoMapper;
using Limak.Application.DTOs.UserPositionDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class UserPositionAutoMapper:Profile
{
    public UserPositionAutoMapper()
    {
        CreateMap<UserPosition, UserPositionGetDto>().ReverseMap(); 
        CreateMap<UserPosition, UserPositionPostDto>().ReverseMap(); 
        CreateMap<UserPosition, UserPositionPutDto>().ReverseMap(); 
    }
}
