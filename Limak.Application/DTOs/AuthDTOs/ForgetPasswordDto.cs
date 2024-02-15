using System.Security.Principal;

namespace Limak.Application.DTOs.AuthDTOs;

public class ForgetPasswordDto
{
    public string Email { get; set; } = null!;
}

