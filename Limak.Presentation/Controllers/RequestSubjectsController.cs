using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.CountryDTOs;
using Limak.Application.DTOs.RequestSubjectDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("requestSubjects")]
[ApiController]
public class RequestSubjectsController : ControllerBase
{
    private readonly IRequestSubjectService _service;

    public RequestSubjectsController(IRequestSubjectService service)
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
    public async Task<IActionResult> CreateAsync([FromForm] RequestSubjectPostDto dto)
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
    public async Task<IActionResult> UpdateAsync(RequestSubjectPutDto dto)
    {

        return Ok(await _service.UpdateAsync(dto));
    }
}
