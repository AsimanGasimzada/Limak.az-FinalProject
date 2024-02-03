using Limak.Application.DTOs.ShopDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IShopService
{
    Task<List<ShopGetDto>> GetAllAsync(int page=1);
    Task<ShopGetDto> GetByIdAsync(int id);
    Task CreateAsync(ShopPostDto dto);
    Task UpdateAsync(ShopPutDto dto);
    Task DeleteAsync(int id);
}
