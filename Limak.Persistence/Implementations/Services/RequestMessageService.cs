using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestMessageDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Implementations.Hubs;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.AspNetCore.SignalR;

namespace Limak.Persistence.Implementations.Services;

public class RequestMessageService : IRequestMessageService
{
    private readonly IHubContext<RequestHub> _requestHub;
    private readonly IRequestMessageRepository _repository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IAuthService _authService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IRequestService _requestService;
    public RequestMessageService(IHubContext<RequestHub> requestHub, IRequestMessageRepository repository, IMapper mapper, INotificationService notificationService, IAuthService authService, ICloudinaryService cloudinaryService, IRequestService requestService)
    {
        _requestHub = requestHub;
        _repository = repository;
        _mapper = mapper;
        _notificationService = notificationService;
        _authService = authService;
        _cloudinaryService = cloudinaryService;
        _requestService = requestService;
    }

    public async Task<ResultDto> SendAsync(RequestMessagePostDto dto)
    {
        var user = await _authService.GetCurrentUserAsync();

        var request = await _requestService.GetByIdAsync(dto.RequestId);

        if (request.AppUserId != user.Id && request.OperatorId != user.Id)
            throw new NotFoundException($"{dto.RequestId}-request is not found");

        var message = _mapper.Map<RequestMessage>(dto);
        message.AppUserId = user.Id;
        

        await _repository.CreateAsync(message);
        await _repository.SaveAsync();

        var userConnectionIds = RequestHub.Connections.FirstOrDefault(x => x.UserId == request.AppUserId)?.ConnectionIds;
        if (userConnectionIds is not null)
        {
            foreach (var connectionId in userConnectionIds)
            {
                await _requestHub.Clients.Client(connectionId).SendAsync("ReceiverequestMessage", dto);
            }
        }

        var MemberConnectionIds = RequestHub.Connections.FirstOrDefault(x => x.UserId == request.OperatorId)?.ConnectionIds;
        if (MemberConnectionIds is not null)
        {
            foreach (var connectionId in MemberConnectionIds)
            {
                await _requestHub.Clients.Client(connectionId).SendAsync("ReceiverequestMessage", dto);
            }
        }


        return new("RequestMessage is successfully created");
    }
}
