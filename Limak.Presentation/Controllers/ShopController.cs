using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.ShopDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("[controller]")]
[ApiController]
public class ShopController : ControllerBase
{
    private readonly IShopService _service;

    public ShopController(IShopService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromForm]ShopPostDto dto)
    {
        await _service.CreateAsync(dto);
        return Ok();
    }

    [HttpDelete("/{id}")]
    public async Task<IActionResult> Delete([FromRoute]int id)
    {
        await _service.DeleteAsync(id);
        return Ok();
    }
}
