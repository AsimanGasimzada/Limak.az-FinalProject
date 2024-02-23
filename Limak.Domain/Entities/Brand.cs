using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Brand:BaseEntity
{
    public string Name { get; set; } = null!;
    public string  ImagePath { get; set; }=null!;
    public string WebsitePath { get; set; } = null!;

}
