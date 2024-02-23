using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CategoryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;


    public CategoryService(ICategoryRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ResultDto> CreateAsync(CategoryPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"This category is already exist({dto.Name})!");
        dto.Image.ValidateImage(2);

        var category = _mapper.Map<Category>(dto);
        category.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        await _repository.CreateAsync(category);
        await _repository.SaveAsync();

        return new($"{category.Name}-Category is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        Category category = await _getCategory(id);

        _repository.SoftDelete(category);
        await _repository.SaveAsync();
        await _cloudinaryService.FileDeleteAsync(category.ImagePath);

        return new($"{category.Name}-Category is successfully deleted");

    }


    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _repository.GetAll(includes:"ShopCategories.Shop").ToListAsync();
        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);
        return dtos;
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _getCategory(id);
        var dto = _mapper.Map<CategoryGetDto>(category);
        return dto;
    }

    public async Task<List<CategoryGetDto>> GetTrash()
    {
        var categories = await _repository.GetFiltered(x => x.IsDeleted, true, "ShopCategories.Shop").ToListAsync();
        if (categories.Count is 0)
            throw new NotFoundException("Trash is empty");

        var dtos=_mapper.Map<List<CategoryGetDto>>(categories);

        return dtos;
    }


    public async Task<ResultDto> RepairDelete(int id)
    {
        var category=await _repository.GetSingleAsync(x=>x.Id == id,true);
        if(category is null)
            throw new NotFoundException($"{id}-category is not found");

        _repository.Repair(category);
        await _repository.SaveAsync();

        return new($"{category.Name}-Category is successfully repair");
    }
    public async Task<bool> IsExistAsync(int id)
    {
        return await _repository.IsExistAsync(x => x.Id == id);
    }

    public async Task<ResultDto> UpdateAsync(CategoryPutDto dto)
    {
        var existedCategory = await _getCategory(dto.Id);
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim() && x.Id != dto.Id);
        if (isExist)
            throw new AlreadyExistException($"{dto.Name}-This Category is already exist!");

        existedCategory = _mapper.Map(dto, existedCategory);
        if (dto.Image is not null)
        {
            dto.Image.ValidateImage();
            await _cloudinaryService.FileDeleteAsync(existedCategory.ImagePath);
            existedCategory.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
        }
        _repository.Update(existedCategory);
        await _repository.SaveAsync();
        return new($"{existedCategory.Name}-Category is successfully updated");
    }

    private async Task<Category> _getCategory(int id)
    {
        var category = await _repository.GetSingleAsync(x => x.Id == id,false, "ShopCategories.Shop");
        if (category is null)
            throw new NotFoundException($"Category not found({id})!");
        return category;
    }
}
