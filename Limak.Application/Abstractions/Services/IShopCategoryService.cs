using Limak.Domain.Entities;

namespace Limak.Application.Abstractions.Services;

public interface IShopCategoryService
{
    Task CreateAsync(Shop shop, int categoryId);
    Task RemoveAsync(int id);
}
