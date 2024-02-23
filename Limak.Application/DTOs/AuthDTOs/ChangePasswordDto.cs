namespace Limak.Application.DTOs.AuthDTOs;

public class ChangePasswordDto
{
    public string ExistPassword { get; set; } = null!;
    public string NewPassword { get; set; }=null!;
    public string ConfirmNewPassword { get; set; }=null !;
}
