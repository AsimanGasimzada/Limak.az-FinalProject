using Limak.Application.DTOs.DeliveryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IDeliveryService
{
    Task<ResultDto> CreateDeliveryAsync(DeliveryPostDto dto);
    Task<ResultDto> CancelDeliveryAsync(int id);
    Task<List<DeliveryGetDto>> GetAllAsync();
    Task<DeliveryGetDto> GetByIdAsync(int id);
    Task<List<DeliveryGetDto>> GetAllAdminAsync(int page=1);
}
