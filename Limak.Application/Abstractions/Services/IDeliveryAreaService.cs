using Limak.Application.DTOs.DeliveryAreaDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IDeliveryAreaService
{
    Task<List<DeliveryAreaGetDto>> GetAllAsync();
    Task<DeliveryAreaGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(DeliveryAreaPostDto dto);
    Task<ResultDto> UpdateAsync(DeliveryAreaPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
}
