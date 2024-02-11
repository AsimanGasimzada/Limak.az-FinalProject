using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.NotificationDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Implementations.Hubs;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class NotificationService : INotificationService
{
    private readonly INotificationRepository _repository;
    private readonly IMapper _mapper;
    private readonly IHubContext<NotificationHub> _notificationHub;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAuthService _authService;
    public NotificationService(INotificationRepository repository, IMapper mapper, IHubContext<NotificationHub> notificationHub, UserManager<AppUser> userManager, IAuthService authService)
    {
        _repository = repository;
        _mapper = mapper;
        _notificationHub = notificationHub;
        _userManager = userManager;
        _authService = authService;
    }

    public async Task<ResultDto> CreateAsync(NotificationPostDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.AppUserId.ToString());

        if (user is null)
            throw new InvalidInputException();

        var notification = _mapper.Map<Notification>(dto);
        await _repository.CreateAsync(notification);
        await _repository.SaveAsync();



        var connectionIds = NotificationHub.Connections.FirstOrDefault(x => x.UserId == dto.AppUserId)?.ConnectionIds;
        if (connectionIds is not null)
        {
            foreach (var connectionId in connectionIds)
            {
                await _notificationHub.Clients.Client(connectionId).SendAsync("ReceiveNotificationMessage", dto);
            }
        }

        return new("Notification successfully sended");
    }


    public async Task<List<NotificationGetDto>> GetAllAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        var notifications = await _repository.GetFiltered(x => x.AppUserId == user.Id).ToListAsync();
        if (notifications.Count is 0)
            throw new NotFoundException("Notification is not found");

        var dtos = _mapper.Map<List<NotificationGetDto>>(notifications);

        return dtos;
    }

    public async Task<NotificationGetDto> GetByIdAsync(int id)
    {
        var user = await _authService.GetCurrentUserAsync();
        Notification? notification = await _getNotification(id, user);

        var dto = _mapper.Map<NotificationGetDto>(notification);

        return dto;
    }

    private async Task<Notification?> _getNotification(int id, AppUserGetDto user)
    {
        var notification = await _repository.GetSingleAsync(x => x.Id == id && x.AppUserId == user.Id);
        if (notification is null)
            throw new NotFoundException("Notification is not found");
        return notification;
    }

    public async Task<ResultDto> ReadAllNotificationsAsync()
    {
        var user = await _authService.GetCurrentUserAsync();
        var notifications = await _repository.GetFiltered(x => x.AppUserId == user.Id && !x.IsRead).ToListAsync();
        notifications.ForEach(x =>
        {
            x.IsRead = true;
            _repository.Update(x);
        });
        await _repository.SaveAsync();

        return new("All Notification are readed!");
    }

    public async Task<ResultDto> ReadNotificationAsync(int id)
    {
        var user = await _authService.GetCurrentUserAsync();
        var notification = await _repository.GetSingleAsync(x => x.Id == id && x.AppUserId == user.Id);
        if (notification is null)
            throw new NotFoundException($"{id}-Notification is not found!");

        notification.IsRead = true;
        _repository.Update(notification);

        return new("Notificiation is successfully readed");

    }


}
