using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.StatusDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class StatusService:IStatusService
{
    private readonly IStatusRepository _repository;
    private readonly IMapper _mapper;
    public StatusService(IStatusRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }



    public async Task<ResultDto> CreateAsync(StatusPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This Status is already exist!");

        var Status = _mapper.Map<Status>(dto);

        await _repository.CreateAsync(Status);
        await _repository.SaveAsync();

        return new($"{Status.Name}-Status is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var Status = await _getStatus(id);

        _repository.HardDelete(Status);
        await _repository.SaveAsync();

        return new($"{Status.Name}-Status is successfully deleted");
    }

    public async Task<List<StatusGetDto>> GetAllAsync()
    {
        var countries = await _repository.GetAll().ToListAsync();

        if (countries.Count is 0)
            throw new NotFoundException("Status is not found");

        var dtos = _mapper.Map<List<StatusGetDto>>(countries);
        return dtos;
    }

    public async Task<StatusGetDto> GetByIdAsync(int id)
    {
        var Status = await _getStatus(id);
        var dto = _mapper.Map<StatusGetDto>(Status);
        return dto;
    }


    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(StatusPutDto dto)
    {
        var existedStatus = await _getStatus(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This Status is already exist!");

        existedStatus = _mapper.Map(dto, existedStatus);
        _repository.Update(existedStatus);
        await _repository.SaveAsync();

        return new($"{existedStatus.Name}-Status is successfully updated");
    }


    private async Task<Status> _getStatus(int id)
    {
        var Status = await _repository.GetSingleAsync(x => x.Id == id);
        if (Status is null)
            throw new NotFoundException($"Status is not found({id})!");
        return Status;
    }
}
