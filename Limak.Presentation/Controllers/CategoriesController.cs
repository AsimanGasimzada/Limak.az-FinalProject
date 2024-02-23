using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CategoryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("categories")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoriesController(ICategoryService service)
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
    public async Task<IActionResult> CreateAsync([FromForm] CategoryPostDto dto)
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
    public async Task<IActionResult> UpdateAsync([FromForm] CategoryPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }

    [HttpGet("[action]")]
    [Authorize(Roles ="Admin,Moderator")]
    public async Task<IActionResult> GetTrash()
    {
        return Ok(await _service.GetTrash());   
    }


    [HttpPatch("[action]/{id}")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> RepairDelete(int id)
    {
        return Ok(await _service.RepairDelete(id));
    }
}
