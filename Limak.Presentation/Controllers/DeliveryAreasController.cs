using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.DeliveryAreaDTOs;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("deliveryAreas")]
[ApiController]
public class DeliveryAreasController : ControllerBase
{
    private readonly IDeliveryAreaService _service;

    public DeliveryAreasController(IDeliveryAreaService service)
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
    public async Task<IActionResult> CreateAsync([FromForm] DeliveryAreaPostDto dto)
    {

        return Ok(await _service.CreateAsync(dto));
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync([FromRoute] int id)
    {

        return Ok(await _service.DeleteAsync(id));
    }
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([FromForm] DeliveryAreaPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}
