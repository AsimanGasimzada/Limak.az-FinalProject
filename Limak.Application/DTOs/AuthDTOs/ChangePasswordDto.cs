namespace Limak.Application.DTOs.AuthDTOs;

public class ChangePasswordDto
{
    public string ExistPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmNewPassword { get; set; }
}
