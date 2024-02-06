using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.OrderDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("orders")]
[ApiController]
[Authorize]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _service;

    public OrdersController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    [Authorize(Roles ="Admin")]
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
    public async Task<IActionResult> CreateAsync( OrderPostDto dto)
    {

        return Ok(await _service.CreateAsync(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {

        return Ok(await _service.DeleteAsync(id));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] OrderPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}
