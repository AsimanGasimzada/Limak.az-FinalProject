using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.DTOs.StatusDTOs;

public class StatusGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public ICollection<OrderRelationDto> Orders { get; set; } = new List<OrderRelationDto>();

}
