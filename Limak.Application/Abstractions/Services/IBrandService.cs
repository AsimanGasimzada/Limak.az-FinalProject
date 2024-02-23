using Limak.Application.DTOs.BrandDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IBrandService
{
    Task<List<BrandGetDto>> GetAllAsync(string? search, int page = 1);
    Task<BrandGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(BrandPostDto dto);
    Task<ResultDto> UpdateAsync(BrandPutDto dto);
    Task<ResultDto> DeleteAsync(int id);
}
