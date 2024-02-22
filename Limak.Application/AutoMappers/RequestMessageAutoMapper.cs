using AutoMapper;
using Limak.Application.DTOs.RequestMessageDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class RequestMessageAutoMapper : Profile
{
    public RequestMessageAutoMapper()
    {
        CreateMap<RequestMessage, RequestMessageGetDto>().ReverseMap();
        CreateMap<RequestMessage, RequestMessageRelationDto>().ReverseMap();
        CreateMap<RequestMessage, RequestMessagePostDto>().ReverseMap();
    }
}
