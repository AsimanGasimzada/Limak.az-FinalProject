using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Notification:BaseAuditableEntity
{
    public string Subject { get; set; } = null!;
    public string Title { get; set; }= null!;
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }= null!;
    public bool IsRead { get; set; }=false;
}
