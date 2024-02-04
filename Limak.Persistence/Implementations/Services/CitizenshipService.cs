using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CitizenshipDTOs;
using Limak.Application.DTOs.CitizenshipDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class CitizenshipService : ICitizenshipService
{
    private readonly ICitizenshipRepository _repository;
    private readonly IMapper _mapper;
    public CitizenshipService(ICitizenshipRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<ResultDto> CreateAsync(CitizenshipPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This Citizenship is already exist!");

        var Citizenship = _mapper.Map<Citizenship>(dto);

        await _repository.CreateAsync(Citizenship);
        await _repository.SaveAsync();

        return new($"{Citizenship.Name}-Citizenship is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var Citizenship = await _getCitizenship(id);

        _repository.HardDelete(Citizenship);
        await _repository.SaveAsync();

        return new($"{Citizenship.Name}-Citizenship is successfully deleted");
    }

    public async Task<List<CitizenshipGetDto>> GetAllAsync()
    {
        var countries = await _repository.GetAll().ToListAsync();

        if (countries.Count is 0)
            throw new NotFoundException("Citizenship is not found");

        var dtos = _mapper.Map<List<CitizenshipGetDto>>(countries);
        return dtos;
    }

    public async Task<CitizenshipGetDto> GetByIdAsync(int id)
    {
        var Citizenship = await _getCitizenship(id);
        var dto = _mapper.Map<CitizenshipGetDto>(Citizenship);
        return dto;
    }


    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(CitizenshipPutDto dto)
    {
        var existedCitizenship = await _getCitizenship(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This Citizenship is already exist!");

        existedCitizenship = _mapper.Map(dto, existedCitizenship);
        _repository.Update(existedCitizenship);
        await _repository.SaveAsync();

        return new($"{existedCitizenship.Name}-Citizenship is successfully updated");
    }


    private async Task<Citizenship> _getCitizenship(int id)
    {
        var Citizenship = await _repository.GetSingleAsync(x => x.Id == id);
        if (Citizenship is null)
            throw new NotFoundException($"Citizenship is not found({id})!");
        return Citizenship;
    }
}
