namespace Limak.Application.DTOs.AuthDTOs;

public class ChangePasswordByAdminDto
{
    public int AppUserId { get; set; }
    public string NewPassword { get; set; } = null!;
    public string ConfirmNewPassword { get; set; } = null!;
}