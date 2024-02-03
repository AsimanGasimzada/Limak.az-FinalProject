namespace Limak.Domain.Entities.Common;

public abstract class BaseAuditableEntity:BaseEntity
{
    public DateTime CreatedTime { get; set; }
    public string CreatedBy { get; set; }
    public DateTime ModifiedTime { get; set; }
    public string ModifiedBy { get; set; }
    public bool IsDeleted { get; set; } = false;
}
