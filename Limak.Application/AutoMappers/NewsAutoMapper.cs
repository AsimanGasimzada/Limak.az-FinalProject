using AutoMapper;
using Limak.Application.DTOs.NewsDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class NewsAutoMapper : Profile
{
    public NewsAutoMapper()
    {
        CreateMap<News, NewsGetDto>().ReverseMap();
        CreateMap<News, NewsPutDto>().ReverseMap();
        CreateMap<News, NewsPostDto>().ReverseMap();
    }
}
