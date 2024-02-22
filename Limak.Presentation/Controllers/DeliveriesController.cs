using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.DeliveryDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("deliveries")]
[ApiController]
[Authorize]
public class DeliveriesController : ControllerBase
{
    private readonly IDeliveryService _service;

    public DeliveriesController(IDeliveryService service)
    {
        _service = service;
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
    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetAllAdmin([FromQuery] int page = 1)
    {
        return Ok(await _service.GetAllAdminAsync(page));
    }


    [HttpPost]
    public async Task<IActionResult> CreateAsync(DeliveryPostDto dto)
    {
        return Ok(await _service.CreateDeliveryAsync(dto));
    }

    [HttpDelete("/{id}")]
    [Authorize(Roles ="Admin,Moderator")]
    public async Task<IActionResult> CancelDeliveryAsync(int id)
    {
        return Ok(await _service.CancelDeliveryAsync(id));  
    }
}
