namespace Limak.Application.DTOs.AuthDTOs;

public class ChangeEmailDto
{
    public int Id { get; set; }
    public string Token { get; set; } = null!;
    public string Email { get; set; } = null!;
}
