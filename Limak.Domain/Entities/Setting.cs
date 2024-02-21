using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Setting : BaseEntity
{
    public string Key { get; set; }
    public string Value { get; set; }
}