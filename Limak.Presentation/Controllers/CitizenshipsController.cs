using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CitizenshipDTOs;
using Limak.Application.DTOs.CountryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("citizenships")]
[ApiController]
public class CitizenshipsController : ControllerBase
{
    private readonly ICitizenshipService _service;

    public CitizenshipsController(ICitizenshipService service)
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
    public async Task<IActionResult> CreateAsync([FromForm] CitizenshipPostDto dto)
    {

        return Ok(await _service.CreateAsync(dto));
    }

    [HttpPut]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> UpdateAsync([FromForm] CitizenshipPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}
