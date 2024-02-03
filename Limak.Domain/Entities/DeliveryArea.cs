using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class DeliveryArea:BaseAuditableEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; }
    public ICollection<Delivery> Deliveries{ get; set; }

}
