using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class RequestSubject : BaseEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Request> Requests { get; set; } = new List<Request>();
}