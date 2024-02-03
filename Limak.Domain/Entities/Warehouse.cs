using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Warehouse : BaseAuditableEntity
{
    public string Name { get; set; }
    public string Location { get; set; }
    public string Position { get; set; }
    public int PhoneNumber { get; set; }
    public string Email { get; set; }
    public string WorkingHours { get; set; }

    public ICollection<Order> Orders { get; set; }
    public ICollection<DeliveryArea> DeliveryAreas{ get; set; }


}
