using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Chat : BaseAuditableEntity
{
    public AppUser AppUser { get; set; } = null!;
    public int AppUserId { get; set; }
    public AppUser? Operator { get; set; }
    public int? OperatorId { get; set; }
    public string? Feedback { get; set; }
    public bool IsActive { get; set; } = true;
    public ICollection<Message> Messages { get; set; } = new List<Message>();
}
    