using AutoMapper;
using Limak.Application.DTOs.NotificationDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class NotificationAutoMapper:Profile
{
    public NotificationAutoMapper()
    {
        CreateMap<Notification, NotificationGetDto>().ReverseMap();
        CreateMap<Notification, NotificationPostDto>().ReverseMap();
            
    }
}
