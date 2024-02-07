using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Company : BaseEntity
{
    public decimal AZNBalance { get; set; } = 0;
    public decimal TRYBalance { get; set; } = 0;
    public decimal USDBalance { get; set; } = 0;
}
