using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.StatusDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("statuses")]
[ApiController]
public class StatusesController : ControllerBase
{
    private readonly IStatusService _service;

    public StatusesController(IStatusService service)
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
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> CreateAsync([FromForm] StatusPostDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

  
}
