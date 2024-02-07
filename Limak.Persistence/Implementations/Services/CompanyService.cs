using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Common;

namespace Limak.Persistence.Implementations.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task<Company> GetCompanyAsync()
    {
        var company = await _repository.GetSingleAsync(x=>x.Id==1);
        if (company is null)
            throw new NotFoundException("Bad reques,contact the developer :(");
        return company;
    }

    public async Task<ResultDto> UpdateCompanyAsync(Company company)
    {
        _repository.Update(company);
        await _repository.SaveAsync();
        return new ResultDto("Success");
    }
}
