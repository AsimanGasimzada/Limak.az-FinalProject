using AutoMapper;
using Limak.Application.DTOs.RequestDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class RequestAutoMapper : Profile
{
    public RequestAutoMapper()
    {
        CreateMap<Request, RequestGetDto>().ReverseMap();
        CreateMap<Request, RequestPostDto>().ReverseMap();
        CreateMap<Request, RequestPutDto>().ReverseMap();
        CreateMap<Request, RequestRelationDto>().ReverseMap();
    }
}
