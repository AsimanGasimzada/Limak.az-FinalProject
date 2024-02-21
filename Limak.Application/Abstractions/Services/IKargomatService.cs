using Limak.Application.DTOs.KargomatDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IKargomatService
{

    Task<List<KargomatGetDto>> GetAllAsync();
    Task<KargomatGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(KargomatPostDto dto);
    Task<ResultDto> UpdateAsync(KargomatPutDto dto);
    Task<bool> IsExistAsync(int id);
    Task<ResultDto> DeleteAsync(int id);
}
