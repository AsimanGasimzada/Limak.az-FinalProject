using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.WarehouseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class WarehouseService : IWarehouseService
{
    private readonly IWarehouseRepository _repository;
    private readonly IMapper _mapper;

    public WarehouseService(IWarehouseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResultDto> CreateAsync(WarehousePostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This warehouse is already exist!");
        var warehouse = _mapper.Map<Warehouse>(dto);
        await _repository.CreateAsync(warehouse);
        await _repository.SaveAsync();

        return new($"{warehouse.Name}-Warehouse is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var warehouse = await _getWarehouse(id);

        _repository.SoftDelete(warehouse);
        await _repository.SaveAsync();

        return new($"{warehouse.Name}-warehouse is successfully deleted");
    }


    public async Task<List<WarehouseGetDto>> GetAllAsync()
    {
        var warehouses = await _repository.GetAll().ToListAsync();

        if (warehouses.Count is 0)
            throw new NotFoundException("Warehouse is not found");

        var dtos = _mapper.Map<List<WarehouseGetDto>>(warehouses);

        return dtos;
    }

    public async Task<WarehouseGetDto> GetByIdAsync(int id)
    {
        var warehouse = await _getWarehouse(id);
        var dto = _mapper.Map<WarehouseGetDto>(warehouse);
        return dto;
    }

    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(WarehousePutDto dto)
    {
        var existedWarehouse = await _getWarehouse(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id!=dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This warehouse is already exist!");
        existedWarehouse = _mapper.Map(dto, existedWarehouse);

        _repository.Update(existedWarehouse);
        await _repository.SaveAsync();
        return new($"{existedWarehouse.Name}-Warehouse is successfully updated");
    }

    private async Task<Warehouse> _getWarehouse(int id)
    {
        var warehouse = await _repository.GetSingleAsync(x => x.Id == id);
        if (warehouse is null)
            throw new NotFoundException($"This warehouse is not found({id})!");
        return warehouse;
    }
}
