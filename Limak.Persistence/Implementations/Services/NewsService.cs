using AutoMapper;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.NewsDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;
using Limak.Persistence.Utilities.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Limak.Persistence.Implementations.Services;

public class NewsService : INewsService
{
    private readonly INewsRepository _repository;
    private readonly IMapper _mapper;
    private readonly ICloudinaryService _cloudinaryService;
    public NewsService(INewsRepository repository, IMapper mapper, ICloudinaryService cloudinaryService)
    {
        _repository = repository;
        _mapper = mapper;
        _cloudinaryService = cloudinaryService;
    }

    public async Task<ResultDto> CreateAsync(NewsPostDto dto)
    {
        dto.Image.ValidateImage(2);

        var news = _mapper.Map<News>(dto);
        news.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);

        await _repository.CreateAsync(news);
        await _repository.SaveAsync();

        return new($"{news.Subject}-News is successfully created");
    }

    public async Task<ResultDto> DeleteAsync(int id)
    {
        var news = await _getNews(id);

        _repository.SoftDelete(news);
        await _repository.SaveAsync();

        return new($"{news.Subject}-News is successfully deleted");
    }



    public async Task<List<NewsGetDto>> GetAllAsync(string? search, int page = 1)
    {
        if (page < 1)
            throw new InvalidInputException();

        var query = _repository.GetFiltered(p => !string.IsNullOrWhiteSpace(search) ? p.Subject.Contains(search) : true);
        var news = await _repository.Paginate(query, 12, page).ToListAsync();
        if (news.Count is 0)
            throw new NotFoundException("News not found!");

        var dtos = _mapper.Map<List<NewsGetDto>>(news);

        return dtos;
    }

    public async Task<NewsGetDto> GetByIdAsync(int id)
    {
        var news = await _getNews(id);

        var dto = _mapper.Map<NewsGetDto>(news);

        return dto;
    }

    public async Task<ResultDto> UpdateAsync(NewsPutDto dto)
    {

        var existed = await _getNews(dto.Id);
        if (dto.Image is not null)
        {
            dto.Image.ValidateImage(2);
            existed.ImagePath = await _cloudinaryService.FileCreateAsync(dto.Image);
        }

        existed = _mapper.Map(dto, existed);

        _repository.Update(existed);
        await _repository.SaveAsync();

        return new($"{existed.Subject}-News is successfully updated");
    }

    private async Task<News> _getNews(int id)
    {
        var news = await _repository.GetSingleAsync(x => x.Id == id);
        if (news is null)
            throw new NotFoundException($"{id}-News is not found");
        return news;
    }

}
