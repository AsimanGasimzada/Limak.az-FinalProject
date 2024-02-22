using Limak.Application.DTOs.AuthDTOs;

namespace Limak.Application.DTOs.NotificationDTOs;

public class NotificationGetDto
{
    public int Id { get; set; }
    public string Subject { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int AppUserId { get; set; }
    public AppUserRelationDto AppUser { get; set; } = null!;
    public DateTime CreatedTime { get; set; }

}
