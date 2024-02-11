using Limak.Application.Abstractions.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("notifications")]
[ApiController]
[Authorize]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _service;

    public NotificationsController(INotificationService service)
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

    [HttpPatch("[action]")]
    public async Task<IActionResult> ReadAllAsync()
    {
        return Ok(await _service.ReadAllNotificationsAsync());
    }


    [HttpPatch("[action]/{id}")]
    public async Task<IActionResult> ReadAsync([FromRoute]int id)
    {
        return Ok(await _service.ReadNotificationAsync(id));
    }

}
