using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.RequestDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("requests")]
[ApiController]
[Authorize]
public class RequestsController : ControllerBase
{
    private readonly IRequestService _service;

    public RequestsController(IRequestService service)
    {
        _service = service;
    }


    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        return Ok(await _service.GetByIdAsync(id));
    }

    [HttpPost]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> CreateAsync(RequestPostDto dto)
    {
        return Ok(await _service.CreateAsync(dto));
    }

    [HttpGet]
    [Authorize(Roles = "Member")]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await _service.GetUsersAllRequestsAsync());
    }

    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetAllAdmin()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpPatch("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> ChangeStatus(RequestPutDto dto)
    {
        return Ok(await _service.UpdateAsync(dto));
    }

    [HttpPatch("[action]/{requestId}")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> SetupOperator([FromRoute] int requestId)
    {
        return Ok(await _service.SetOperatorAsync(requestId));
    }


    [HttpGet("[action]")]
    [Authorize(Roles = "Admin,Moderator")]
    public async Task<IActionResult> GetWithoutAnOperatorRequests()
    {
        return Ok(await _service.GetWithoutAnOperatorRequests());
    }

}
