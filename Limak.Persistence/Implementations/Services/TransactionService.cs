using AutoMapper;
using ClosedXML.Excel;
using DocumentFormat.OpenXml.Office2016.Drawing.Command;
using Limak.Application.Abstractions.Repositories;
using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.Common;
using Limak.Application.DTOs.RepsonseDTOs;
using Limak.Application.DTOs.TransactionDTOs;
using Limak.Domain.Entities;
using Limak.Persistence.Utilities.Exceptions.Identity;
using Limak.Persistence.Utilities.Exceptions.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Limak.Persistence.Implementations.Services;

public class TransactionService : ITransactionService
{
    private readonly ITransactionRepository _repository;
    private readonly ICompanyService _companyService;
    private readonly UserManager<AppUser> _userManager;
    private readonly IAuthService _authService;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly IMapper _mapper;
    public TransactionService(ICompanyService companyService, UserManager<AppUser> userManager, IHttpContextAccessor contextAccessor, IMapper mapper, ITransactionRepository repository, IAuthService authService)
    {
        _companyService = companyService;
        _userManager = userManager;
        _contextAccessor = contextAccessor;
        _mapper = mapper;
        _repository = repository;
        _authService = authService;
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

        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.AZNBalance,
            Amount = dto.Amount,
            IsPayment = false,
            Feedback = "Increase AZN Balance"

        };
        await _repository.CreateAsync(transaction);
        await _repository.SaveAsync();

        return new($"{user.Name} {user.Surname}-AZN Balance:{user.AZNBalance}");
    }

    public async Task<ResultDto> IncreaseAZNBalanceAdmin(BalanceAdminPutDto dto)
    {
        var user = await GetUserById(dto.AppUserId);

        user.AZNBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.AZNBalance,
            Amount = dto.Amount,
            IsPayment = false,
            Feedback = "RePayment AZN Balance"

        };
        await _repository.CreateAsync(transaction);
        await _repository.SaveAsync();


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

        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.TRYBalance,
            Amount = dto.Amount,
            IsPayment = false,
            Feedback = $"Increase TRY Balance"

        };
        await _repository.CreateAsync(transaction);
        await _repository.SaveAsync();


        return new($"{user.Name} {user.Surname}-TRY Balance:{user.TRYBalance}");
    }

    public async Task<ResultDto> IncreaseTRYBalanceAdmin(BalanceAdminPutDto dto)
    {
        var user = await GetUserById(dto.AppUserId);

        user.TRYBalance += dto.Amount;
        await _userManager.UpdateAsync(user);


        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.TRYBalance,
            Amount = dto.Amount,
            IsPayment = false,
            Feedback = $"Repayment TRY Balance"

        };
        await _repository.CreateAsync(transaction);
        await _repository.SaveAsync();


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

        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.USDBalance,
            Amount = dto.Amount,
            IsPayment = false,
            Feedback = $"Increase USD Balance"

        };
        await _repository.CreateAsync(transaction);
        await _repository.SaveAsync();


        return new($"{user.Name} {user.Surname}-USD Balance:{user.USDBalance}");
    }

    public async Task<ResultDto> IncreaseUSDBalanceAdmin(BalanceAdminPutDto dto)
    {

        var user = await GetUserById(dto.AppUserId);

        user.USDBalance += dto.Amount;
        await _userManager.UpdateAsync(user);

        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.AZNBalance,
            Amount = dto.Amount,
            IsPayment = false,
            Feedback = $"Repayment USD Balance"

        };
        await _repository.CreateAsync(transaction);
        await _repository.SaveAsync();



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

        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.AZNBalance,
            Amount = dto.Amount,
            IsPayment = true,
            Feedback = $"Payment AZN Balance"

        };
        await _repository.CreateAsync(transaction);
        //çağırıldığı yerdə save olunmalıdır


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


        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.TRYBalance,
            Amount = dto.Amount,
            IsPayment = true,
            Feedback = $"Payment TRY Balance,Orders{string.Join(",", dto.OrderIds ?? new())}"
        };

        await _repository.CreateAsync(transaction);
        //çağırıldığı yerdə save olunmalıdır

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

        Transaction transaction = new()
        {
            AppUserId = user.Id,
            UserBalance = user.TRYBalance,
            Amount = dto.Amount,
            IsPayment = true,
            Feedback = $"Payment TRY Balance,Orders{string.Join(",", dto.OrderIds ?? new())}"
        };

        await _repository.CreateAsync(transaction);
        //çağırıldığı yerdə save olunmalıdır


        var company = await _companyService.GetCompanyAsync();
        company.USDBalance += dto.Amount;
        await _companyService.UpdateCompanyAsync(company);

        return new($"{user.Name} {user.Surname}-USD Balance:{user.USDBalance}-Payment is successfull!");
    }



    public async Task<ExportExcelDto> ExportToExcelAsync()
    {
        var currentUser = await _authService.GetCurrentUserAsync();
        var transactions = await _repository.GetFiltered(x => x.AppUserId == currentUser.Id, false, "AppUser").OrderByDescending(x => x.Id) .ToListAsync();



        using (var workBook = new XLWorkbook())
        {
            IXLWorksheet worksheet = workBook.Worksheets.Add("Ödənişlər");
            worksheet.Cell(1, 1).Value = "Id";
            worksheet.Cell(1, 2).Value = "Date/Time";
            worksheet.Cell(1, 3).Value = "Amount";
            worksheet.Cell(1, 4).Value = "Feedback";
            worksheet.Cell(1, 5).Value = "UserBalance";



            worksheet.Column(1).Width = 8;
            worksheet.Column(2).Width = 20;
            worksheet.Column(3).Width = 8;
            worksheet.Column(4).Width = 40;
            worksheet.Column(5).Width = 20;


            for (int i = 0; i < transactions.Count; i++)
            {
                var transaction = transactions[i];

                worksheet.Cell(i + 2, 1).Value = transaction.Id;
                worksheet.Cell(i + 2, 2).Value = transaction.CreatedTime;
                worksheet.Cell(i + 2, 3).Value = transaction.Amount;
                worksheet.Cell(i + 2, 4).Value = transaction.Feedback;
                worksheet.Cell(i + 2, 5).Value = transaction.UserBalance;


            }

            using (var stream = new MemoryStream())
            {
                workBook.SaveAs(stream);
                var content = stream.ToArray();
                return new() { FileContents = content, ConcentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", FileName = "Ödənişlər" };

            }

        }
    }

    public async Task<List<TransactionGetDto>> GetAllTransactions()
    {
        var currentUser = await _authService.GetCurrentUserAsync();
        var transactions = await _repository.GetFiltered(x => x.AppUserId == currentUser.Id, false, "AppUser").OrderByDescending(x => x.Id).ToListAsync();

        var dtos = _mapper.Map<List<TransactionGetDto>>(transactions);
        return dtos;
    }

    private async Task<AppUser> GetUser()
    {
        var id = _contextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier);
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
