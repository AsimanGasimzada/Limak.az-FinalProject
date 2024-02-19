using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestMessageDTOs;
using Limak.Persistence.Implementations.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Limak.Persistence.Implementations.Services;

public class RequestMessageService : IRequestMessageService
{
    private readonly IHubContext<RequestHub> _requestHub;
    private readonly IRequestMessageRepository _repository;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    private readonly IAuthService _authService;
    public RequestMessageService(IHubContext<RequestHub> requestHub, IRequestMessageRepository repository, IMapper mapper, INotificationService notificationService, IAuthService authService)
    {
        _requestHub = requestHub;
        _repository = repository;
        _mapper = mapper;
        _notificationService = notificationService;
        _authService = authService;
    }

    public Task<ResultDto> SendAsync(RequestMessagePostDto dto)
    {
        throw new NotImplementedException();
    }
}
