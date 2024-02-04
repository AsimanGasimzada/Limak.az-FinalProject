using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CategoryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _repository;
    private readonly IMapper _mapper;



    public CategoryService(ICategoryRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<ResultDto> CreateAsync(CategoryPostDto dto)
    {
        var isExist = await _repository.IsExistAsync(x => x.Name.ToLower() == dto.Name.ToLower().Trim());
        if (isExist)
            throw new AlreadyExistException($"This category is already exist({dto.Name})!");

        var category = _mapper.Map<Category>(dto);

        await _repository.CreateAsync(category);
        await _repository.SaveAsync();

        return new($"{category.Name}-Category is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        Category category = await _getCategory(id);

        _repository.HardDelete(category);
        await _repository.SaveAsync();
        return new($"{category.Name}-Category is successfully deleted");

    }


    public async Task<List<CategoryGetDto>> GetAllAsync()
    {
        var categories = await _repository.GetAll().ToListAsync();
        var dtos = _mapper.Map<List<CategoryGetDto>>(categories);
        return dtos;
    }

    public async Task<CategoryGetDto> GetByIdAsync(int id)
    {
        var category = await _getCategory(id);
        var dto = _mapper.Map<CategoryGetDto>(category);
        return dto;
    }

    public async Task<bool> IsExist(int id)
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
        _repository.Update(existedCategory);
        await _repository.SaveAsync();
        return new($"{existedCategory.Name}-Category is successfully created");
    }

    private async Task<Category> _getCategory(int id)
    {
        var category = await _repository.GetSingleAsync(x => x.Id == id);
        if (category is null)
            throw new NotFoundException($"Category not found({id})!");
        return category;
    }
}
