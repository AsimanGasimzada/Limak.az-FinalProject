using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.SettingDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class SettingService : ISettingService
{
    private readonly ISettingRepository _repository;
    private readonly IMapper _mapper;

    public SettingService(ISettingRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResultDto> CreateAsync(SettingPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Key.ToLower() == dto.Key.ToLower());
        if (isExist)
            throw new ConflictException($"{dto.Key}-this setting is already exist");

        var setting = _mapper.Map<Setting>(dto);

        await _repository.CreateAsync(setting);
        await _repository.SaveAsync();

        return new("Setting successfully created");
    }

    public async Task<Dictionary<string,string>> GetAllAsync()
    {
        var settings = await _repository.GetAll().ToListAsync();

        if (settings.Count is 0)
            throw new NotFoundException("Setting is not found");

        var dictionary = settings.ToDictionary(x => x.Key, x => x.Value);

        return dictionary;
    }

    public async Task<SettingGetDto> GetByIdAsync(int id)
    {
        var setting=await _repository.GetSingleAsync(x=>x.Id== id);
        if(setting is null)
            throw new NotFoundException($"{id}-Setting is not found");

        var dto=_mapper.Map<SettingGetDto>(setting);

        return dto;

    }

    public async Task<ResultDto> UpdateAsync(SettingPutDto dto)
    {
        var setting = await _repository.GetSingleAsync(x => x.Id == dto.Id);
        if (setting is null)
            throw new NotFoundException($"{dto.Id}-Setting is not found");

        setting = _mapper.Map(dto, setting);

        _repository.Update(setting);
        await _repository.SaveAsync();

        return new($"{setting.Key}-setting is successfully edited");

    }
}
