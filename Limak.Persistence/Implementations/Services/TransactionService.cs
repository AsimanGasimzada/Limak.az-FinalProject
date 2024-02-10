using AutoMapper;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.TransactionDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Limak.Persistence.Utilities.Exceptions.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Limak.Persistence.Implementations.Services;

public class TransactionService : ITransactionService
{
    private readonly ICompanyService _companyService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;
    public TransactionService(ICompanyService companyService, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IMapper mapper)
    {
        _companyService = companyService;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
    }

    public async Task<GetBalanceDto> GetBalances()
    {
        var user = await GetUser();
        var dto = _mapper.Map<GetBalanceDto>(user);
        return dto;
    }

    public async Task<ResultDto> IncreaseAZNBalance(BalancePutDto dto)
    {
        var user = await GetUser();

        user.AZNBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        return new($"{user.Name} {user.Surname}-AZN Balance:{user.AZNBalance}");
    }

    public async Task<ResultDto> IncreaseAZNBalanceAdmin(BalanceAdminPutDto dto)
    {
        var user = await GetUserById(dto.AppUserId);

        user.AZNBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        var company = await _companyService.GetCompanyAsync();
        company.AZNBalance -= dto.Amount;
        await _companyService.UpdateCompanyAsync(company);

        return new($"{user.Name} {user.Surname}-AZN Balance:{user.AZNBalance}");
    }

    public async Task<ResultDto> IncreaseTRYBalance(BalancePutDto dto)
    {
        var user = await GetUser();

        user.TRYBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        return new($"{user.Name} {user.Surname}-TRY Balance:{user.TRYBalance}");
    }

    public async Task<ResultDto> IncreaseTRYBalanceAdmin(BalanceAdminPutDto dto)
    {
        var user = await GetUserById(dto.AppUserId);

        user.TRYBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        var company = await _companyService.GetCompanyAsync();
        company.TRYBalance -= dto.Amount;
        await _companyService.UpdateCompanyAsync(company);

        return new($"{user.Name} {user.Surname}-TRY Balance:{user.TRYBalance}");
    }

    public async Task<ResultDto> IncreaseUSDBalance(BalancePutDto dto)
    {
        var user = await GetUser();

        user.USDBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        return new($"{user.Name} {user.Surname}-USD Balance:{user.USDBalance}");
    }

    public async Task<ResultDto> IncreaseUSDBalanceAdmin(BalanceAdminPutDto dto)
    {

        var user = await GetUserById(dto.AppUserId);

        user.USDBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        var company = await _companyService.GetCompanyAsync();
        company.USDBalance -= dto.Amount;
        await _companyService.UpdateCompanyAsync(company);
        return new($"{user.Name} {user.Surname}-USD Balance:{user.USDBalance}");
    }

    public async Task<ResultDto> PaymentByAZNBalance(BalancePutDto dto)
    {
        var user = await GetUser();
        if (user.AZNBalance < dto.Amount)
            throw new NotEnoughBalanceException($"{user.Name} {user.Surname}-AZN Balance:{user.AZNBalance} but amount:{dto.Amount}!");

        user.AZNBalance -= dto.Amount;
        await _userManager.UpdateAsync(user);

        var company = await _companyService.GetCompanyAsync();
        company.AZNBalance += dto.Amount;
        await _companyService.UpdateCompanyAsync(company);

        return new($"{user.Name} {user.Surname}-AZN Balance:{user.AZNBalance}-Payment is successfull!");
    }

    public async Task<ResultDto> PaymentByTRYBalance(BalancePutDto dto)
    {
        var user = await GetUser();
        if (user.TRYBalance < dto.Amount)
            throw new NotEnoughBalanceException($"{user.Name} {user.Surname}-TRY Balance:{user.TRYBalance} but amount:{dto.Amount}!");

        user.TRYBalance -= dto.Amount;
        await _userManager.UpdateAsync(user);

        var company = await _companyService.GetCompanyAsync();
        company.TRYBalance += dto.Amount;
        await _companyService.UpdateCompanyAsync(company);

        return new($"{user.Name} {user.Surname}-TRY Balance:{user.TRYBalance}-Payment is successfull!");

    }

    public async Task<ResultDto> PaymentByUSDBalance(BalancePutDto dto)
    {
        var user = await GetUser();
        if (user.USDBalance < dto.Amount)
            throw new NotEnoughBalanceException($"{user.Name} {user.Surname}-USD Balance:{user.USDBalance} but amount:{dto.Amount}!");

        user.USDBalance -= dto.Amount;
        await _userManager.UpdateAsync(user);

        var company = await _companyService.GetCompanyAsync();
        company.USDBalance += dto.Amount;
        await _companyService.UpdateCompanyAsync(company);

        return new($"{user.Name} {user.Surname}-USD Balance:{user.USDBalance}-Payment is successfull!");
    }

    private async Task<AppUser> GetUser()
    {
        var id = _contextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = await _userManager.FindByIdAsync(id);
        if (user is null)
            throw new UnAuthorizedException();
        return user;
    }
    private async Task<AppUser> GetUserById(int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
            throw new UnAuthorizedException();
        return user;
    }
}
