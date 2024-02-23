using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Exceptions.Identity;
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
        var requests = await _repository.GetAll(false, "AppUser", "Operator", "RequestSubject", "RequestMessages", "Country").ToListAsync();
        var dtos = _mapper.Map<List<RequestGetDto>>(requests);
        return dtos;
    }

    public async Task<List<RequestGetDto>> GetUsersAllRequestsAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        var requests = await _repository.GetFiltered(x => x.AppUserId == user.Id, false, "AppUser", "Operator", "RequestSubject", "RequestMessages","Country").ToListAsync();
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


    public async Task<RequestGetDto> GetByIdAsync(int id)
    {
        var request = await _repository.GetSingleAsync(x => x.Id == id, false, "RequestSubject", "RequestMessages", "Operator", "AppUser", "Country");

        if (request is null)
            throw new NotFoundException($"{id}-This Request is not found");

        request.RequestMessages=request.RequestMessages.OrderBy(x=>x.CreatedTime).ToList();

        var dto = _mapper.Map<RequestGetDto>(request);

        return dto;
    }

    public async Task<ResultDto> SetOperatorAsync(int requestId)
    {
        var user = await _authService.GetCurrentUserAsync();
        var role = await _authService.GetUserRoleAsync(user.Id);

        if (role is "Member")
            throw new UnAuthorizedException("A member cannot be an operator");

        var request = await _repository.GetSingleAsync(x => x.Id == requestId, false, "RequestSubject", "RequestMessages", "Operator", "AppUser", "Country");
        if (request is null)
            throw new NotFoundException($"{requestId}-This Request is not found");

        if (request.OperatorId is not null)
            throw new ConflictException("This request already has an operator");

        request.OperatorId = user.Id;

        _repository.Update(request);
        await _repository.SaveAsync();

        return new("Succesfully setup");

    }



    public async Task<List<RequestGetDto>> GetWithoutAnOperatorRequests()
    {

        var requests=await _repository.GetFiltered(x=>x.OperatorId==0 || x.OperatorId==null,false, "RequestSubject", "RequestMessages", "Operator", "AppUser", "Country").ToListAsync();

        if (requests.Count is 0)
            throw new NotFoundException("Request is not found");

        var dtos=_mapper.Map<List<RequestGetDto>>(requests);

        return dtos;
    }
}
