using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.MessageDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("messages")]
[ApiController]
[Authorize]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _service;

    public MessagesController(IMessageService service)
    {
        _service = service;
    }


    [HttpPost]
    public async Task<IActionResult> SendAsync([FromForm]MessagePostDto dto)
    {
        return Ok(await _service.SendMessageAsync(dto));
    }

}
