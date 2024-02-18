using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.RequestSubjectDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.Abstractions.Services;

public interface IRequestSubjectService
{
    Task<List<RequestSubjectGetDto>> GetAllAsync();
    Task<RequestSubjectGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(RequestSubjectPostDto dto);
    Task<ResultDto> UpdateAsync(RequestSubjectPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
}
