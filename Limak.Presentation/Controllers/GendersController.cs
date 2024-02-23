using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CitizenshipDTOs;
using Limak.Application.DTOs.GenderDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("genders")]
[ApiController]
public class GendersController : ControllerBase
{
    private readonly IGenderService _service;

    public GendersController(IGenderService service)
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
    public async Task<IActionResult> CreateAsync([FromForm] GenderPostDto dto)
    {

        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPut]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> UpdateAsync([FromForm] GenderPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}
