using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.DeliveryAreaDTOs;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> CreateAsync([FromForm] DeliveryAreaPostDto dto)
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
    public async Task<IActionResult> UpdateAsync([FromForm] DeliveryAreaPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }


    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
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
