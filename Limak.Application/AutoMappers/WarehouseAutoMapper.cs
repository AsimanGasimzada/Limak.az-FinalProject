using AutoMapper;
using Limak.Application.DTOs.WarehouseDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.AutoMappers;

public class WarehouseAutoMapper:Profile
{
    public WarehouseAutoMapper()
    {
        CreateMap<Warehouse, WarehouseGetDto>().ReverseMap();
        CreateMap<Warehouse, WarehousePostDto>().ReverseMap();
        CreateMap<Warehouse, WarehousePutDto>().ReverseMap();
        CreateMap<Warehouse, WarehouseRelationDto>().ReverseMap();
    }
}
