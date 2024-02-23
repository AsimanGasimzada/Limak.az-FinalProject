using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.ChatDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("chats")]
[ApiController]
[Authorize]
public class ChatsController : ControllerBase
{
    private readonly IChatService _service;

    public ChatsController(IChatService service)
    {
        _service = service;
    }

    [HttpPost]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> CreateAsync()
    {
        return Ok(await _service.CreateAsync());
    }
    [HttpPut]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> UpdateAsync(ChatPutDto dto)
    {
        return Ok(await _service.UpdateAsync(dto));
    }
    [HttpGet]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetAll());
    }


    [HttpGet("[action]")]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> GetOnlineChat()
    {
        return Ok(await _service.GetOnlineChatAsync());
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }


    [HttpPut("[action]/{id}")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> SetOperator([FromRoute]int id)
    {
        return Ok(await _service.SetOperatorAsync(id));
    }




    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetWithoutAnOperatorChats()
    {
        return Ok(await _service.GetWithoutAnOperatorChats());
    }

}
