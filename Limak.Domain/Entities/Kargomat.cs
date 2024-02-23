using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Kargomat : BaseAuditableEntity
{
    public string Location { get; set; } = null!;
    public string CordinateX { get; set; } = null!;
    public string CordinateY { get; set; } = null!;
    public decimal Price { get; set; }

    public ICollection<Order> Orders { get; set; }=new List<Order>();

}
