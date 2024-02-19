using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class RequestService : IRequestService
{
    private readonly IRequestRepository _repository;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IRequestSubjectService _requestSubjectService;
    public RequestService(IRequestRepository repository, IMapper mapper, IAuthService authService, IRequestSubjectService requestSubjectService)
    {
        _repository = repository;
        _mapper = mapper;
        _authService = authService;
        _requestSubjectService = requestSubjectService;
    }

    public async Task<ResultDto> CreateAsync(RequestPostDto dto)
    {
        var user = await _authService.GetCurrentUserAsync();
        var isExistSubject = await _requestSubjectService.IsExist(dto.RequestSubjectId);

        if (!isExistSubject)
            throw new NotFoundException($"{dto.RequestSubjectId}-This subject is not found!");



        var request = _mapper.Map<Request>(dto);
        request.AppUserId = user.Id;
        await _repository.CreateAsync(request);
        await _repository.SaveAsync();

        return new("Request successfully created");
    }

    public async Task<List<RequestGetDto>> GetAllAsync()
    {
        var requests = await _repository.GetAll().ToListAsync();
        var dtos = _mapper.Map<List<RequestGetDto>>(requests);
        return dtos;
    }

    public async Task<List<RequestGetDto>> GetUsersAllRequestsAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        var requests = await _repository.GetFiltered(x => x.AppUserId == user.Id).ToListAsync();
        var dtos = _mapper.Map<List<RequestGetDto>>(requests);
        return dtos;
    }

    public async Task<ResultDto> UpdateAsync(RequestPutDto dto)
    {
        var request = await _repository.GetSingleAsync(x => x.Id == dto.Id);
        if (request is null)
            throw new NotFoundException($"{dto.Id}-This request is not found");

        request = _mapper.Map(dto, request);

        _repository.Update(request);
        await _repository.SaveAsync();

        return new($"{dto.Id}-Request is successfully updated");
    }
}
