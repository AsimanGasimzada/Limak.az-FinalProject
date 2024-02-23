using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.BrandDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    public BrandService(IBrandRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ResultDto> CreateAsync(BrandPostDto dto)
    {
        dto.Image.ValidateImage(2);

        var brand = _mapper.Map<Brand>(dto);
        brand.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        await _repository.CreateAsync(brand);
        await _repository.SaveAsync();

        return new($"{brand.Name}-Brand is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var brand = await _getBrand(id);

        _repository.HardDelete(brand);
        await _repository.SaveAsync();

        return new($"{brand.Name}-News is successfully deleted");
    }

    public async Task<List<BrandGetDto>> GetAllAsync(string? search, int page = 1)
    {
        if (page < 1)
            throw new InvalidInputException();

        var query = _repository.GetFiltered(x => !string.IsNullOrWhiteSpace(search) ? x.Name.Contains(search) : true);
        var brands = await _repository.Paginate(query, 12, page).ToListAsync();
        if (brands.Count is 0)
            throw new NotFoundException("News not found!");

        var dtos = _mapper.Map<List<BrandGetDto>>(brands);

        return dtos;
    }

    public async Task<BrandGetDto> GetByIdAsync(int id)
    {
        var brand = await _getBrand(id);

        var dto = _mapper.Map<BrandGetDto>(brand);

        return dto;
    }

    public async Task<ResultDto> UpdateAsync(BrandPutDto dto)
    {
        var existed = await _getBrand(dto.Id);
        if (dto.Image is not null)
        {
            dto.Image.ValidateImage(2);
            existed.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
        }

        existed = _mapper.Map(dto, existed);

        _repository.Update(existed);
        await _repository.SaveAsync();

        return new($"{existed.Name}-Brand is successfully updated");
    }




    private async Task<Brand> _getBrand(int id)
    {
        var brand = await _repository.GetSingleAsync(x => x.Id == id);
        if (brand is null)
            throw new NotFoundException($"{id}-Brand is not found");
        return brand;
    }
}
