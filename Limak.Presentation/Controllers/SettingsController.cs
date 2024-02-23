using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.SettingDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("settings")]
[ApiController]
public class SettingsController : ControllerBase
{
    private readonly ISettingService _service;

    public SettingsController(ISettingService service)
    {
        _service = service;
    }





    [HttpPost]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> CreateAsync(SettingPostDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }



    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAllAsync());
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync([FromRoute]int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }



    [HttpPut]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> UpdateAsync( SettingPutDto dto)
    {
        return Ok(await _service.UpdateAsync(dto));
    }





}


