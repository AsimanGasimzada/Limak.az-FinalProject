using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class News : BaseAuditableEntity
{
    public string Subject { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
}
