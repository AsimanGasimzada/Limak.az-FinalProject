using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.StatusDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IStatusService
{
    Task<List<StatusGetDto>> GetAllAsync();
    Task<StatusGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(StatusPostDto dto);
    Task<ResultDto> UpdateAsync(StatusPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
    Task<StatusGetDto> GetByNameAsync(string name);
}
