using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Transaction : BaseAuditableEntity
{
    public decimal Amount { get; set; }
    public bool IsPayment { get; set; }
    public string Feedback { get; set; } = null!;
    public decimal UserBalance { get; set; }
    public AppUser AppUser { get; set; } = null!;
    public int AppUserId { get; set; }
}
