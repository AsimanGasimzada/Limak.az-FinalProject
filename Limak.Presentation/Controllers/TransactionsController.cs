using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.StripeDTOs;
using Limak.Application.DTOs.TransactionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("Transactions")]
[ApiController]
[Authorize]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;
    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }




    [HttpPost("[action]")]
    [Authorize(Roles = "Member")]

    public async Task<IActionResult> IncreaseAZNBalance(StripePayDto dto)
    {
        return Ok(await _service.IncreaseAZNBalance(dto));
    }
    [HttpPost("[action]")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> IncreaseUSDBalance(StripePayDto dto)
    {
        return Ok(await _service.IncreaseUSDBalance(dto));
    }
    [HttpPost("[action]")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> IncreaseTRYBalance(StripePayDto dto)
    {
        return Ok(await _service.IncreaseTRYBalance(dto));
    }

    [HttpGet]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllTransactions());
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> GetBalances()
    {
        return Ok(await _service.GetBalances());
    }

    [HttpGet("ExportExcel")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> ExportToExcelAsync()
    {
        var result = await _service.ExportToExcelAsync();

        return File(result.FileContents, result.ConcentType, result.FileName);
    }


    //Admin actions
    [HttpPut("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> IncreaseUserAZNBalance(BalanceAdminPutDto dto)
    {
        return Ok(await _service.IncreaseAZNBalanceAdmin(dto));
    }



    [HttpPut("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> IncreaseUserTRYBalance(BalanceAdminPutDto dto)
    {
        return Ok(await _service.IncreaseTRYBalanceAdmin(dto));
    }



    [HttpPut("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> IncreaseUserUSDBalance(BalanceAdminPutDto dto)
    {
        return Ok(await _service.IncreaseUSDBalanceAdmin(dto));
    }
}
