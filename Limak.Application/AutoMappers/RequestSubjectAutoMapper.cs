using AutoMapper;
using Limak.Application.DTOs.RequestSubjectDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class RequestSubjectAutoMapper : Profile
{
    public RequestSubjectAutoMapper()
    {
        CreateMap<RequestSubject, RequestSubjectGetDto>().ReverseMap();
        CreateMap<RequestSubject, RequestSubjectPostDto>().ReverseMap();
        CreateMap<RequestSubject, RequestSubjectPutDto>().ReverseMap();
        CreateMap<RequestSubject, RequestSubjectRelationDto>().ReverseMap();
    }
}
