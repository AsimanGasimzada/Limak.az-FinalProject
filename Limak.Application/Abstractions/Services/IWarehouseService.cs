using Limak.Application.DTOs.CategoryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.WarehouseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IWarehouseService
{
    Task<List<WarehouseGetDto>> GetAllAsync();
    Task<WarehouseGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(WarehousePostDto dto);
    Task<ResultDto> UpdateAsync(WarehousePutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
}
