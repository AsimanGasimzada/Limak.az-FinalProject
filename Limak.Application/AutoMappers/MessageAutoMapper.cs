using AutoMapper;
using Limak.Application.DTOs.MessageDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class MessageAutoMapper:Profile
{
    public MessageAutoMapper()
    {
        CreateMap<Message, MessagePostDto>().ReverseMap();
        CreateMap<Message, MessageGetDto>().ReverseMap();
    }
}
