using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CountryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("countries")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly ICountryService _service;

    public CountriesController(ICountryService service)
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
    

    [HttpPut]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> UpdateAsync(CountryPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}
