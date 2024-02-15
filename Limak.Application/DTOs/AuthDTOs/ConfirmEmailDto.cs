namespace Limak.Application.DTOs.AuthDTOs;

public class ConfirmEmailDto
{
    public string AppUserId { get; set; } = null!;
    public string Token { get; set; } = null!;
}
