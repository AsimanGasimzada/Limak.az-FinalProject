using Limak.Application.DTOs.DeliveryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IDeliveryService
{
    Task<ResultDto> CreateDeliveryAsync(DeliveryPostDto dto);
    Task<ResultDto> CancelDeliveryAsync(int id);
}
