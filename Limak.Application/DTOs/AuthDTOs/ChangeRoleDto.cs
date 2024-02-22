namespace Limak.Application.DTOs.AuthDTOs;

public class ChangeRoleDto
{
    public int AppUserId { get; set; }
    public string Role { get; set; } = null!;

}
