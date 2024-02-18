using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class RequestMessage : BaseAuditableEntity
{
    public AppUser AppUser { get; set; } = null!;
    public int AppUserId { get; set; }
    public Request Request { get; set; } = null!;
    public int RequestId { get; set; }
    public string Message { get; set; } = null!;

}