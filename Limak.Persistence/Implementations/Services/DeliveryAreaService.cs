using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.DeliveryAreaDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class DeliveryAreaService:IDeliveryAreaService
{
    private readonly IDeliveryAreaRepository _repository;
    private readonly IMapper _mapper;
    private readonly IWarehouseService _warehouseService;
    public DeliveryAreaService(IDeliveryAreaRepository repository, IMapper mapper, IWarehouseService warehouseService)
    {
        _repository = repository;
        _mapper = mapper;
        _warehouseService = warehouseService;
    }

    public async Task<ResultDto> CreateAsync(DeliveryAreaPostDto dto)
    {
        if(!await _warehouseService.IsExist(dto.WarehouseId))
            throw new NotFoundException($"This warehouse is not found({dto.WarehouseId})!");

        var DeliveryArea = _mapper.Map<DeliveryArea>(dto);
        await _repository.CreateAsync(DeliveryArea);
        await _repository.SaveAsync();

        return new($"{DeliveryArea.Name}-DeliveryArea is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var DeliveryArea = await _getDeliveryArea(id);

        _repository.SoftDelete(DeliveryArea);
        await _repository.SaveAsync();

        return new($"{DeliveryArea.Name}-DeliveryArea is successfully deleted");
    }


    public async Task<List<DeliveryAreaGetDto>> GetAllAsync()
    {
        var DeliveryAreas = await _repository.GetAll().ToListAsync();

        if (DeliveryAreas.Count is 0)
            throw new NotFoundException("DeliveryArea is not found");

        var dtos = _mapper.Map<List<DeliveryAreaGetDto>>(DeliveryAreas);

        return dtos;
    }

    public async Task<DeliveryAreaGetDto> GetByIdAsync(int id)
    {
        var DeliveryArea = await _getDeliveryArea(id);
        var dto = _mapper.Map<DeliveryAreaGetDto>(DeliveryArea);
        return dto;
    }

    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(DeliveryAreaPutDto dto)
    {
        var existedDeliveryArea = await _getDeliveryArea(dto.Id);

        if (!await _warehouseService.IsExist(dto.WarehouseId))
            throw new NotFoundException($"This warehouse is not found({dto.WarehouseId})!");

        existedDeliveryArea = _mapper.Map(dto, existedDeliveryArea);

        _repository.Update(existedDeliveryArea);
        await _repository.SaveAsync();
        return new($"{existedDeliveryArea.Name}-DeliveryArea is successfully updated");
    }

    private async Task<DeliveryArea> _getDeliveryArea(int id)
    {
        var DeliveryArea = await _repository.GetSingleAsync(x => x.Id == id);
        if (DeliveryArea is null)
            throw new NotFoundException($"This DeliveryArea is not found({id})!");
        return DeliveryArea;
    }
}
