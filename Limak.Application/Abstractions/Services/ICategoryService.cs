using Limak.Application.DTOs.CategoryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(CategoryPostDto dto);
    Task<ResultDto> UpdateAsync(CategoryPutDto dto);
    Task<bool> IsExistAsync(int id);
    Task<ResultDto> DeleteAsync(int id);
}
