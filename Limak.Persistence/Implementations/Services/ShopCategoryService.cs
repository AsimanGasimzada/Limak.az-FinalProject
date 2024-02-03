using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Domain.Entities;

namespace Limak.Persistence.Implementations.Services;

public class ShopCategoryService : IShopCategoryService
{
    private readonly IShopCategoryRepository _repository;

    public ShopCategoryService(IShopCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Shop shop, int categoryId)
    {
        ShopCategory shopCategory = new()
        {
            CategoryId=categoryId,
            Shop=shop
        };
        await _repository.CreateAsync(shopCategory);
    }

    public async Task RemoveAsync(int id)
    {
        var sc=await _repository.GetSingleAsync(x=>x.Id==id);
        if (sc!=null)
        {
            _repository.HardDelete(sc);
        }
    }
}
