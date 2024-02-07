using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;

namespace Limak.Application.Abstractions.Services;

public interface ICompanyService
{
    Task<Company> GetCompanyAsync();
    Task<ResultDto> UpdateCompanyAsync(Company company);
}
