using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Warehouse : BaseAuditableEntity
{
    public string Name { get; set; } = null!;   
    public string Location { get; set; } = null!;
    public string Position { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string WorkingHours { get; set; } = null!;

    public ICollection<Order> Orders { get; set; } = new List<Order>();
    public ICollection<DeliveryArea> DeliveryAreas { get; set; } = new List<DeliveryArea>();


}
