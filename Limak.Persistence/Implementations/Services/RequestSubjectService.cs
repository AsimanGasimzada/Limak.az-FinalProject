using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestSubjectDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class RequestSubjectService : IRequestSubjectService
{
    private readonly IRequestSubjectRepository _repository;
    private readonly IMapper _mapper;
    public RequestSubjectService(IRequestSubjectRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResultDto> CreateAsync(RequestSubjectPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This RequestSubject is already exist!");

        var RequestSubject = _mapper.Map<RequestSubject>(dto);

        await _repository.CreateAsync(RequestSubject);
        await _repository.SaveAsync();

        return new($"{RequestSubject.Name}-RequestSubject is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var RequestSubject = await _getRequestSubject(id);

        _repository.SoftDelete(RequestSubject);
        await _repository.SaveAsync();

        return new($"{RequestSubject.Name}-RequestSubject has been successfully trashed");
    }


    public async Task<List<RequestSubjectGetDto>> GetAllAsync()
    {
        var countries = await _repository.GetAll().ToListAsync();

        if (countries.Count is 0)
            throw new NotFoundException("RequestSubject is not found");

        var dtos = _mapper.Map<List<RequestSubjectGetDto>>(countries);
        return dtos;
    }

    public async Task<RequestSubjectGetDto> GetByIdAsync(int id)
    {
        var RequestSubject = await _getRequestSubject(id);
        var dto = _mapper.Map<RequestSubjectGetDto>(RequestSubject);
        return dto;
    }


    public async Task<bool> IsExist(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(RequestSubjectPutDto dto)
    {
        var existedRequestSubject = await _getRequestSubject(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This RequestSubject is already exist!");

        existedRequestSubject = _mapper.Map(dto, existedRequestSubject);
        _repository.Update(existedRequestSubject);
        await _repository.SaveAsync();

        return new($"{existedRequestSubject.Name}-RequestSubject is successfully updated");
    }


    private async Task<RequestSubject> _getRequestSubject(int id)
    {
        var RequestSubject = await _repository.GetSingleAsync(x => x.Id == id);
        if (RequestSubject is null)
            throw new NotFoundException($"RequestSubject is not found({id})!");
        return RequestSubject;
    }

}
