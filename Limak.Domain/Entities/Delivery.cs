using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Delivery:BaseAuditableEntity
{
    public string Region { get; set; } = null!;
    public string Village { get; set; }=null!;
    public string Street { get; set; } = null!;
    public string HomeNo { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Notes { get; set; }=null !;
    public bool IsCancel { get; set; } = false;
    public AppUser AppUser{ get; set; } = null!;
    public int AppUserId{ get; set; }
    public int DeliveryAreaId { get; set; }
    public DeliveryArea DeliveryArea { get; set; } = null!;
    public ICollection<Order> Orders { get; set; }=new List<Order>();
}
