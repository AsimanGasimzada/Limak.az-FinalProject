using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Tariff : BaseAuditableEntity
{
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public decimal Price { get; set; }
    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;
}
