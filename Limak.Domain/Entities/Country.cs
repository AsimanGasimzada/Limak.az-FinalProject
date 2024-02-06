using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Country:BaseEntity
{
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }

}
