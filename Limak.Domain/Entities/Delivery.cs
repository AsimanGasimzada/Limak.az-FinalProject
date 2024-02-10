using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Delivery:BaseAuditableEntity
{
    public string Region { get; set; }
    public string Village { get; set; }
    public string Street { get; set; }
    public string HomeNo { get; set; }
    public string Phone { get; set; }
    public string Notes { get; set; }
    public bool IsCancel { get; set; } = false;
    public int DeliveryAreaId { get; set; }
    public DeliveryArea DeliveryArea { get; set; }
    public ICollection<Order> Orders { get; set; }
}
