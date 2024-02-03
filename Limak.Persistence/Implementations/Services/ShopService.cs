using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.ShopDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class ShopService : IShopService
{
    private readonly IShopCategoryService _shopCategoryService;
    private readonly ICategoryService _categoryService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IShopRepository _repository;
    private readonly IMapper _mapper;

    public ShopService(IShopRepository repository, IMapper mapper, ICloudinaryService cloudinaryService, IShopCategoryService shopCategoryService, ICategoryService categoryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _shopCategoryService = shopCategoryService;
        _categoryService = categoryService;
    }

    public async Task CreateAsync(ShopPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException("Shop is already exist!");
        foreach (var categoryId in dto.CategoryIds)
        {
            if (!await _categoryService.IsExist(categoryId))
                throw new NotFoundException($"This Category is not found({categoryId})!");
        }

        dto.Image.ValidateImage();
        var shop = _mapper.Map<Shop>(dto);
        shop.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        foreach (var id in dto.CategoryIds)
        {
            await _shopCategoryService.CreateAsync(shop, id);
        }

        await _repository.CreateAsync(shop);
        await _repository.SaveAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var shop = await _getShop(id);
        _repository.HardDelete(shop);
        await _cloudinaryService.FileDeleteAsync(shop.ImagePath);
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

        var shop = await _repository.GetSingleAsync(x => x.Id == id, false, "ShopCategories","Orders");

        if (shop is null)
            throw new NotFoundException("Shop is not found");

        return shop;
    }
    #endregion
}
