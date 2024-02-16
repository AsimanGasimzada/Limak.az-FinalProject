using AutoMapper;
using Limak.Application.DTOs.ChatDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class ChatAutoMapper : Profile
{
    public ChatAutoMapper()
    {
        CreateMap<Chat, ChatGetDto>().ReverseMap();
        CreateMap<Chat, ChatPutDto>().ReverseMap();

    }
}
