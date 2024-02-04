using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.UserPositionDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace Limak.Persistence.Implementations.Services;

public class UserPositionService : IUserPositionService
{
    private readonly IUserPositionRepository _repository;
    private readonly IMapper _mapper;
    public UserPositionService(IUserPositionRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ResultDto> CreateAsync(UserPositionPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This UserPosition is already exist!");

        var UserPosition = _mapper.Map<UserPosition>(dto);

        await _repository.CreateAsync(UserPosition);
        await _repository.SaveAsync();

        return new($"{UserPosition.Name}-UserPosition is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var UserPosition = await _getUserPosition(id);

        _repository.HardDelete(UserPosition);
        await _repository.SaveAsync();

        return new($"{UserPosition.Name}-UserPosition is successfully deleted");
    }

    public async Task<List<UserPositionGetDto>> GetAllAsync()
    {
        var countries = await _repository.GetAll().ToListAsync();

        if (countries.Count is 0)
            throw new NotFoundException("UserPosition is not found");

        var dtos = _mapper.Map<List<UserPositionGetDto>>(countries);
        return dtos;
    }

    public async Task<UserPositionGetDto> GetByIdAsync(int id)
    {
        var UserPosition = await _getUserPosition(id);
        var dto = _mapper.Map<UserPositionGetDto>(UserPosition);
        return dto;
    }


    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(UserPositionPutDto dto)
    {
        var existedUserPosition = await _getUserPosition(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This UserPosition is already exist!");

        existedUserPosition = _mapper.Map(dto, existedUserPosition);
        _repository.Update(existedUserPosition);
        await _repository.SaveAsync();

        return new($"{existedUserPosition.Name}-UserPosition is successfully updated");
    }


    private async Task<UserPosition> _getUserPosition(int id)
    {
        var UserPosition = await _repository.GetSingleAsync(x => x.Id == id);
        if (UserPosition is null)
            throw new NotFoundException($"UserPosition is not found({id})!");
        return UserPosition;
    }
}
