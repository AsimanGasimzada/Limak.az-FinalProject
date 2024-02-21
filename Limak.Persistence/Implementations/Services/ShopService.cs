using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.ShopDTOs;
using Limak.Domain.Entities;
using Limak.Domain.Enums;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class ShopService : IShopService
{
    private readonly IShopCategoryService _shopCategoryService;
    private readonly ICategoryService _categoryService;
    private readonly ICountryService _countryService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly IShopRepository _repository;
    private readonly IMapper _mapper;

    public ShopService(IShopRepository repository, IMapper mapper, ICloudinaryService cloudinaryService, IShopCategoryService shopCategoryService, ICategoryService categoryService, ICountryService countryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
        _shopCategoryService = shopCategoryService;
        _categoryService = categoryService;
        _countryService = countryService;
    }

    public async Task<ResultDto> CreateAsync(ShopPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException("Shop is already exist!");

        foreach (var categoryId in dto.CategoryIds)
        {
            if (!await _categoryService.IsExistAsync(categoryId))
                throw new NotFoundException($"This Category is not found({categoryId})!");
        }

        dto.Image.ValidateImage();

        if (!await _countryService.IsExist(dto.CountryId))
            throw new NotFoundException($"This Country is not found({dto.CountryId})!");


        var shop = _mapper.Map<Shop>(dto);
        shop.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        foreach (var id in dto.CategoryIds)
        {
            await _shopCategoryService.CreateAsync(shop, id);
        }

        await _repository.CreateAsync(shop);
        await _repository.SaveAsync();

        return new($"{shop.Name}-shop is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var shop = await _getShop(id);
        _repository.HardDelete(shop);
        await _cloudinaryService.FileDeleteAsync(shop.ImagePath);
        await _repository.SaveAsync();

        return new($"{shop.Name}-Shop is successfully deleted");
    }

    public async Task<List<ShopGetDto>> GetAllAsync(int? country, int? category, int page = 1)
    {
        if (page < 1)
            throw new InvalidInputException();

        if (country is null || country is 0)
            country = (await _countryService.GetByNameAsync(CountryNames.Turkey)).Id;


        var query = _repository.GetFiltered(x => x.CountryId == country);
        if (category is not null && category != 0)
            query = query.Where(x => x.ShopCategories.Any(x => x.CategoryId == category));

        var shops = await _repository.Paginate(query, 12, page).ToListAsync();
        if (shops.Count == 0)
            throw new NotFoundException("Shop is not found!");

        var dtos = _mapper.Map<List<ShopGetDto>>(shops);

        return dtos;
    }

    public async Task<ShopGetDto> GetByIdAsync(int id)
    {
        Shop shop = await _getShop(id);

        var dto = _mapper.Map<ShopGetDto>(shop);
        return dto;
    }



    public async Task<ResultDto> UpdateAsync(ShopPutDto dto)
    {
        var existedShop = await _getShop(dto.Id);
        bool isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"This Shop is already exist({dto.Name})!");

        foreach (var categoryId in dto.CategoryIds)
        {
            if (!await _categoryService.IsExistAsync(categoryId))
                throw new NotFoundException($"This Category is not found({categoryId})!");
        }

        if (dto.Image is not null)
            dto.Image.ValidateImage();

        if (!await _countryService.IsExist(dto.CountryId))
            throw new NotFoundException($"This Country is not found({dto.CountryId})!");


        var existedCategoryIds = existedShop.ShopCategories.Select(x => x.CategoryId);

        var deletedCategoryIds = existedCategoryIds.Except(dto.CategoryIds);

        var createdCategoryIds = dto.CategoryIds.Except(existedCategoryIds);

        foreach (var id in deletedCategoryIds)
        {
            await _shopCategoryService.RemoveAsync(existedShop.ShopCategories.FirstOrDefault(x => x.CategoryId == id)?.Id ?? 0);
        }
        foreach (var id in createdCategoryIds)
        {
            await _shopCategoryService.CreateAsync(existedShop, id);
        }

        existedShop = _mapper.Map(dto, existedShop);

        if (dto.Image is not null)
            existedShop.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        _repository.Update(existedShop);
        await _repository.SaveAsync();

        return new($"{existedShop.Name}-Shop is successfully updated");

    }




    #region PrivateMethods
    private async Task<Shop> _getShop(int id)
    {
        if (id < 1)
            throw new InvalidInputException();

        var shop = await _repository.GetSingleAsync(x => x.Id == id, false, "ShopCategories", "Orders", "Country");

        if (shop is null)
            throw new NotFoundException("Shop is not found");

        return shop;
    }
    #endregion
}
