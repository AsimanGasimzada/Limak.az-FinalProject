using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.TransactionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace Limak.Presentation.Controllers;

[Route("Transactions")]
[ApiController]
[Authorize(Roles = "Member")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _service;

    public TransactionsController(ITransactionService service)
    {
        _service = service;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> IncreaseAZNBalance(BalancePutDto dto)
    {
        return Ok(await _service.IncreaseAZNBalance(dto));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> IncreaseUSDBalance(BalancePutDto dto)
    {
        return Ok(await _service.IncreaseUSDBalance(dto));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> IncreaseTRYBalance(BalancePutDto dto)
    {
        return Ok(await _service.IncreaseTRYBalance(dto));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllTransactions());
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetBalances()
    {
        return Ok(await _service.GetBalances());
    }

    [HttpGet("ExportExcel")]
    public async Task<IActionResult> ExportToExcelAsync()
    {


        var result = await _service.ExportToExcelAsync();

        return File(result.FileContents, result.ConcentType, result.FileName);
    }
}
