namespace Limak.Application.DTOs.AuthDTOs;

public class ResetPasswordTokenDto
{
    public string AppUserId { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string Password { get; set; }= null!;
    public string ConfirmPassword { get; set; }=null!;
}

