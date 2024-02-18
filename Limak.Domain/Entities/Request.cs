using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Request : BaseAuditableEntity
{
    public Country Country { get; set; } = null!;
    public int CountryId { get; set; }
    public RequestSubject RequestSubject { get; set; } = null!;
    public int RequestSubjectId { get; set; }
    public bool? Status { get; set; } = false;
}
