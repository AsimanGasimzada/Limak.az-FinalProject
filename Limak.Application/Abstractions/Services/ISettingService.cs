using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.SettingDTOs;

namespace Limak.Application.Abstractions.Services;

public interface ISettingService
{
    Task<ResultDto> CreateAsync(SettingPostDto dto);
    Task<Dictionary<string, string>> GetAllAsync();
    Task<SettingGetDto> GetByIdAsync(int id);
    Task<ResultDto> UpdateAsync(SettingPutDto dto);

}
