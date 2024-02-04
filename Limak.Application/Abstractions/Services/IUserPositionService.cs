using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.UserPositionDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IUserPositionService
{
    Task<List<UserPositionGetDto>> GetAllAsync();
    Task<UserPositionGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(UserPositionPostDto dto);
    Task<ResultDto> UpdateAsync(UserPositionPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
}
