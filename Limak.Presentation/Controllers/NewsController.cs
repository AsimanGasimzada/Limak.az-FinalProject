using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.NewsDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("news")]
[ApiController]
public class NewsController : ControllerBase
{
    private readonly INewsService _service;

    public NewsController(INewsService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAllAsync([FromQuery] string? search,int page=1)
    {
        return Ok(await _service.GetAllAsync(search,page));
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }
    [HttpPost]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> CreateAsync([FromForm] NewsPostDto dto)
    {

        return Ok(await _service.CreateAsync(dto));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {

        return Ok(await _service.DeleteAsync(id));
    }
    [HttpPut]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> UpdateAsync([FromForm] NewsPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}

