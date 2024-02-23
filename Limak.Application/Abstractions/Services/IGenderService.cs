using Limak.Application.DTOs.GenderDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IGenderService
{
    Task<List<GenderGetDto>> GetAllAsync();
    Task<GenderGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(GenderPostDto dto);
    Task<ResultDto> UpdateAsync(GenderPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
    Task<GenderGetDto> FirstOrDefault();
}
