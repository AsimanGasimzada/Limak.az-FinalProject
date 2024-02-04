using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.KargomatDTOs;
using Limak.Application.DTOs.WarehouseDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("kargomats")]
[ApiController]
public class KargomatsController : ControllerBase
{
    private readonly IKargomatService _service;

    public KargomatsController(IKargomatService service)
    {
        _service = service;
    }



    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm] KargomatPostDto dto)
    {

        return Ok(await _service.CreateAsync(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {

        return Ok(await _service.DeleteAsync(id));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] KargomatPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}
