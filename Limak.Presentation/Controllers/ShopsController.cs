using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.ShopDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers
{
    [Route("shops")]
    [ApiController]
    public class ShopsController : ControllerBase
    {
        private readonly IShopService _service;

        public ShopsController(IShopService service)
        {
            _service = service;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] int? country,int? category,int page = 1)
        {
            return Ok(await _service.GetAllAsync(country,category,page));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
        [HttpPost]
        [Authorize(Roles = "Admin,Moderator")]
        public async Task<IActionResult> CreateAsync([FromForm] ShopPostDto dto)
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
        public async Task<IActionResult> UpdateAsync([FromForm] ShopPutDto dto)
        {

            return Ok(await _service.UpdateAsync(dto));
        }
    }
}
