using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.UserPositionDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers
{
    [Route("userPositions")]
    [ApiController]
    public class UserPositionsController : ControllerBase
    {
        private readonly IUserPositionService _service;

        public UserPositionsController(IUserPositionService service)
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
        public async Task<IActionResult> CreateAsync([FromForm] UserPositionPostDto dto)
        {

            return Ok(await _service.CreateAsync(dto));
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> UpdateAsync([FromForm] UserPositionPutDto dto)
        {

            return Ok(await _service.UpdateAsync(dto));
        }
    }
}
