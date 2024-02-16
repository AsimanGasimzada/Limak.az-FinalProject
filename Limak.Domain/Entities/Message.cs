using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Message : BaseAuditableEntity
{
    public AppUser AppUser { get; set; }= null!;    
    public int AppUserId { get; set; }
    public string Body { get; set; } = null!;
    public string? FilePath { get; set; }
    public Chat Chat { get; set; }=null !;
    public int ChatId { get; set; }
}