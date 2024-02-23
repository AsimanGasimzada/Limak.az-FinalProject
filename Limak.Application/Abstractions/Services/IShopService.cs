using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.ShopDTOs;

namespace Limak.Application.Abstractions.Services;

public interface IShopService
{
    Task<List<ShopGetDto>> GetAllAsync(int? country, int? category, int page = 1);
    Task<ShopGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(ShopPostDto dto);
    Task<ResultDto> UpdateAsync(ShopPutDto dto);
    Task<ResultDto> DeleteAsync(int id);
}



