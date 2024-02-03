using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CategoryDTOs;

namespace Limak.Persistence.Implementations.Services;

public class CategoryService : ICategoryService
{
    public Task CreateAsync(CategoryPostDto dto)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<List<CategoryGetDto>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<CategoryGetDto> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(CategoryPutDto dto)
    {
        throw new NotImplementedException();
    }
}
