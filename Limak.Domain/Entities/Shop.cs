using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Shop:BaseAuditableEntity
{
    public string Name { get; set; }
    public string ImagePath { get; set; }
    public string WebsitePath { get; set; }
    public Country Country { get; set; }
    public int CountryId { get; set; }
    public ICollection<ShopCategory> ShopCategories { get; set; }=new List<ShopCategory>();

    public ICollection<Order> Orders { get; set; } = new List<Order>();


}
