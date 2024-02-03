using Limak.Application.DTOs.CategoryDTOs;

namespace Limak.Application.Abstractions.Services;

public interface ICategoryService
{
    Task<List<CategoryGetDto>> GetAllAsync();
    Task<CategoryGetDto> GetByIdAsync(int id);
    Task CreateAsync(CategoryPostDto dto);
    Task UpdateAsync(CategoryPutDto dto);
    Task DeleteAsync(int id);
}
