using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.GenderDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.WarehouseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class GenderService:IGenderService
{
    private readonly IGenderRepository _repository;
    private readonly IMapper _mapper;

    public GenderService(IGenderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ResultDto> CreateAsync(GenderPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This Gender is already exist!");

        var Gender = _mapper.Map<Gender>(dto);

        await _repository.CreateAsync(Gender);
        await _repository.SaveAsync();

        return new($"{Gender.Name}-Gender is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var Gender = await _getGender(id);

        _repository.HardDelete(Gender);
        await _repository.SaveAsync();

        return new($"{Gender.Name}-Gender is successfully deleted");
    }

    public async Task<List<GenderGetDto>> GetAllAsync()
    {
        var countries = await _repository.GetAll().ToListAsync();

        if (countries.Count is 0)
            throw new NotFoundException("Gender is not found");

        var dtos = _mapper.Map<List<GenderGetDto>>(countries);
        return dtos;
    }

    public async Task<GenderGetDto> GetByIdAsync(int id)
    {
        var Gender = await _getGender(id);
        var dto = _mapper.Map<GenderGetDto>(Gender);
        return dto;
    }


    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(GenderPutDto dto)
    {
        var existedGender = await _getGender(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This Gender is already exist!");

        existedGender = _mapper.Map(dto, existedGender);
        _repository.Update(existedGender);
        await _repository.SaveAsync();

        return new($"{existedGender.Name}-Gender is successfully updated");
    }


    public async Task<GenderGetDto> FirstOrDefault()
    {
        var gender = await _repository.GetSingleAsync(x => x.Id > 0, true);
        if (gender is null)
            throw new NotFoundException($"gender is not found");


        var dto = _mapper.Map<GenderGetDto>(gender);

        return dto;
    }

    private async Task<Gender> _getGender(int id)
    {
        var Gender = await _repository.GetSingleAsync(x => x.Id == id);
        if (Gender is null)
            throw new NotFoundException($"Gender is not found({id})!");
        return Gender;
    }
}
