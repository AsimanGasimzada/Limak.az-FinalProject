using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Status:BaseEntity
{
    public string Name { get; set; }

    public ICollection<Order> Orders { get; set; }=new List<Order>();

}
