using Limak.Application.Abstractions.Services;
using Limak.Application.DTOs.AuthDTOs;
using Limak.Application.DTOs.StripeDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Limak.Presentation.Controllers;

[Route("auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _service;
    private readonly IPaymentService _paymentService;
    public AuthController(IAuthService service, IPaymentService paymentService)
    {
        _service = service;
        _paymentService = paymentService;
    }



    [HttpPost("[action]")]
    public async Task<IActionResult> Pay(StripePayDto dto)
    {
        return Ok(await _paymentService.PayAsync(dto));
    }
    [HttpPost("[action]")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        return Ok(await _service.RegisterAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        return Ok(await _service.LoginAsync(dto));
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> CreateRoles()
    {
        return Ok(await _service.CreateRolesAsync());
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> RefreshToken(string refreshToken)
    {
        return Ok(await _service.RefreshToken(refreshToken));
    }


    [HttpPost("[action]")]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
    {
        return Ok(await _service.ChangePasswordAsync(dto));
    }
}
