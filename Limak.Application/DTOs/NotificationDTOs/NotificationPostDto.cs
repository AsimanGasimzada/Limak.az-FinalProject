namespace Limak.Application.DTOs.NotificationDTOs;

public class NotificationPostDto
{

    public string Subject { get; set; } = null!;
    public string Title { get; set; } = null!;
    public int AppUserId { get; set; }
}
