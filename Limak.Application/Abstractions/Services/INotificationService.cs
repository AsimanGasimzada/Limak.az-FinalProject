using Limak.Application.DTOs.NotificationDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface INotificationService
{

    Task<List<NotificationGetDto>> GetAllAsync();
    Task<NotificationGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(NotificationPostDto dto);
    Task<ResultDto> ReadNotificationAsync(int id);
    Task<ResultDto> ReadAllNotificationsAsync();
}
