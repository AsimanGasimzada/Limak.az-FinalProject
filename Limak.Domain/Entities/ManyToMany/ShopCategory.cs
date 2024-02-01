using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class ShopCategory:BaseEntity
{
    public int ShopId { get; set; }
    public Shop Shop { get; set; }
    public int CategoryId { get; set; }
    public Category Category{ get; set; }

}
