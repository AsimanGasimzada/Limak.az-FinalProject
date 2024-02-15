using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.TariffDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("tariffs")]
[ApiController]
public class TariffsController : ControllerBase
{
    private readonly ITariffService _service;

    public TariffsController(ITariffService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("/{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm]TariffPutDto dto)
    {
        return Ok(await _service.UpdateAsync(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute]int id)
    {
        return Ok(await _service.DeleteAsync(id));  
    }
}
