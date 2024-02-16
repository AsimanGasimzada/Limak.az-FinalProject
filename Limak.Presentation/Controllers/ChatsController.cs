using Limak.Application.Abstractions.Services;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("chats")]
[ApiController]
public class ChatsController : ControllerBase
{
    private readonly IChatService _service;

    public ChatsController(IChatService service)
    {
        _service = service;
    }


}
