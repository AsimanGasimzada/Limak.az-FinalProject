using Limak.Application.DTOs.CountryDTOs;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.Abstractions.Services;

public interface ICountryService
{
    Task<List<CountryGetDto>> GetAllAsync();
    Task<CountryGetDto> GetByIdAsync(int id);
    Task<ResultDto> CreateAsync(CountryPostDto dto);
    Task<ResultDto> UpdateAsync(CountryPutDto dto);
    Task<bool> IsExist(int id);
    Task<ResultDto> DeleteAsync(int id);
    Task<Country> FirstOrDefaultAsync();
    Task<CountryGetDto> GetByNameAsync(string name);
}




