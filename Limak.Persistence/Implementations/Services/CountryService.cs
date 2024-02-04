using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CountryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _repository;
    private readonly IMapper _mapper;

    public CountryService(ICountryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResultDto> CreateAsync(CountryPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This country is already exist!");

        var country = _mapper.Map<Country>(dto);

        await _repository.CreateAsync(country);
        await _repository.SaveAsync();

        return new($"{country.Name}-Country is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var country = await _getCountry(id);

        _repository.SoftDelete(country);
        await _repository.SaveAsync();

        return new($"{country.Name}-Country has been successfully trashed");
    }

    public async Task<Country> FirstOrDefaultAsync()
    {
        return await _repository.GetAll().FirstOrDefaultAsync();
    }

    public async Task<List<CountryGetDto>> GetAllAsync()
    {
        var countries = await _repository.GetAll().ToListAsync();

        if (countries.Count is 0)
            throw new NotFoundException("Country is not found");

        var dtos = _mapper.Map<List<CountryGetDto>>(countries);
        return dtos;
    }

    public async Task<CountryGetDto> GetByIdAsync(int id)
    {
        var country = await _getCountry(id);
        var dto = _mapper.Map<CountryGetDto>(country);
        return dto;
    }


    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(CountryPutDto dto)
    {
        var existedCountry = await _getCountry(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This country is already exist!");

        existedCountry = _mapper.Map(dto, existedCountry);
        _repository.Update(existedCountry);
        await _repository.SaveAsync();

        return new($"{existedCountry.Name}-Country is successfully updated");
    }


    private async Task<Country> _getCountry(int id)
    {
        var country = await _repository.GetSingleAsync(x => x.Id == id);
        if (country is null)
            throw new NotFoundException($"Country is not found({id})!");
        return country;
    }

}
