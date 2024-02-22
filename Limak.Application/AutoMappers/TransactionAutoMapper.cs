using AutoMapper;
using Limak.Application.DTOs.TransactionDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class TransactionAutoMapper:Profile
{
    public TransactionAutoMapper()
    {
        CreateMap<AppUser, GetBalanceDto>().ReverseMap();
        CreateMap<Transaction,TransactionRelationDto >().ReverseMap();
        CreateMap<Transaction,TransactionGetDto >().ReverseMap();
    }
}
