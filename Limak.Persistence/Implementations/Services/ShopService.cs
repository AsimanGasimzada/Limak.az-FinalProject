using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.ShopDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class ShopService : IShopService
{
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IShopRepository _repository;
    private readonly IMapper _mapper;

    public ShopService(IShopRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task CreateAsync(ShopPostDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int id)
    {
        var shop = await _getShop(id);
        _repository.HardDelete(shop);
        await _repository.SaveAsync();
    }

    public async Task<List<ShopGetDto>> GetAllAsync(int page = 1)
    {
        if (page < 1)
            throw new InvalidInputException();
        var shops = await _repository.Paginate(_repository.GetAll(), 12, page).ToListAsync();
        if (shops.Count == 0)
            throw new NotFoundException();

        var dtos = _mapper.Map<List<ShopGetDto>>(shops);

        return dtos;
    }

    public async Task<ShopGetDto> GetByIdAsync(int id)
    {
        Shop shop = await _getShop(id);

        var dto = _mapper.Map<ShopGetDto>(shop);
        return dto;
    }



    public Task UpdateAsync(ShopPutDto dto)
    {
        throw new NotImplementedException();
    }




    #region PrivateMethods
    private async Task<Shop> _getShop(int id)
    {
        if (id < 1)
            throw new InvalidInputException();

        var shop = await _repository.GetSingleAsync(x => x.Id == id, false, "Category");

        if (shop is null)
            throw new NotFoundException("Shop is not found");

        return shop;
    }
    #endregion
}
