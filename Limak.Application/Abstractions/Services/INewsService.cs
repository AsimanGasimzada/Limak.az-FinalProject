using Limak.Application.DTOs.NewsDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface INewsService
{
    Task<List<NewsGetDto>> GetAllAsync(int page = 1);
    Task<NewsGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(NewsPostDto dto);
    Task<ResultDto> UpdateAsync(NewsPutDto dto);
    Task<ResultDto> DeleteAsync(int id);
}
