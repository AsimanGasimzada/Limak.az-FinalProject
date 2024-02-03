using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Kargomat : BaseAuditableEntity
{
    public string Location { get; set; }
    public string Position { get; set; }
    public decimal Price { get; set; }

    public ICollection<Order> Orders { get; set; }

}
