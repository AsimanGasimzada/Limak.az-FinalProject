using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RequestMessageDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("requestMessages")]
[ApiController]
[Authorize]
public class RequestMessagesController : ControllerBase
{
    private readonly IRequestMessageService _service;

    public RequestMessagesController(IRequestMessageService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> SendAsync(RequestMessagePostDto dto)
    {
        return Ok(await _service.SendAsync(dto));
    }
}
