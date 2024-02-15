using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.TariffDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class TariffService : ITariffService
{
    private readonly ITariffRepository _repository;
    private readonly ICountryService _countryService;
    private readonly IMapper _mapper;

    public TariffService(ITariffRepository repository, IMapper mapper, ICountryService countryService)
    {
        _repository = repository;
        _mapper = mapper;
        _countryService = countryService;
    }

    public async Task<ResultDto> CreateAsync(TariffPostDto dto)
    {

        var isExistCountry = await _countryService.IsExist(dto.CountryId);
        if (!isExistCountry)
            throw new NotFoundException($"{dto.CountryId}-Country is not found!");

        var isExist = await _repository.IsExistAsync(x => !(x.MaxValue <= dto.MinValue || x.MinValue >= dto.MaxValue));
        if (isExist)
            throw new AlreadyExistException("this range is already exist");

        var tariff = _mapper.Map<Tariff>(dto);
        await _repository.CreateAsync(tariff);
        await _repository.SaveAsync();


        return new($"Tariff is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        Tariff tariff = await _getTariff(id);

        _repository.SoftDelete(tariff);
        await _repository.SaveAsync();

        return new($"{id}-Tariff is successfully created");
    }

    private async Task<Tariff> _getTariff(int id)
    {
        var tariff = await _repository.GetSingleAsync(x => x.Id == id);
        if (tariff is null)
            throw new NotFoundException($"{id}-Tariff is not found!");
        return tariff;
    }

    public async Task<List<TariffGetDto>> GetAllAsync()
    {
        var query = _repository.GetAll(false, "Country");
        var tariffs = await _repository.OrderBy(query, x => x.CountryId).ToListAsync();
        if (tariffs.Count is 0)
            throw new NotFoundException("Tariff is not found!");
        var dtos = _mapper.Map<List<TariffGetDto>>(tariffs);
        return dtos;
    }

    public async Task<TariffGetDto> GetByIdAsync(int id)
    {
        var tariff = await _getTariff(id);

        var dto = _mapper.Map<TariffGetDto>(tariff);

        return dto;
    }

    public async Task<bool> IsExistAsync(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(TariffPutDto dto)
    {
        var existedTariff=await _getTariff(dto.Id);

        var isExistCountry = await _countryService.IsExist(dto.CountryId);
        if (!isExistCountry)
            throw new NotFoundException($"{dto.CountryId}-Country is not found!");

        var isExist = await _repository.IsExistAsync(x => !(x.MaxValue <= dto.MinValue || x.MinValue >= dto.MaxValue) && x.Id!=dto.Id);
        if (isExist)
            throw new AlreadyExistException("this range is already exist");

        existedTariff = _mapper.Map(dto, existedTariff);
        _repository.Update(existedTariff);
        await _repository.SaveAsync();

        return new($"{existedTariff.Id}-Tarif is successfully edited");

    }
}
