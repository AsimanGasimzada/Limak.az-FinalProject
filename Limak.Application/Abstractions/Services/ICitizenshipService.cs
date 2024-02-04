using Limak.Application.DTOs.CitizenshipDTOs;
using Limak.Application.DTOs.RepsonseDTOs;

namespace Limak.Application.Abstractions.Services;

public interface ICitizenshipService
{
    Task<List<CitizenshipGetDto>> GetAllAsync();
    Task<CitizenshipGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(CitizenshipPostDto dto);
    Task<ResultDto> UpdateAsync(CitizenshipPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id); 
}
