using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.DTOs.KargomatDTOs;

public class KargomatGetDto
{

    public int Id { get; set; }
    public string Location { get; set; } = null!;
    public string Position { get; set; }= null!;
    public decimal Price { get; set; }
    public List<OrderRelationDto> Orders { get; set; } = new List<OrderRelationDto>();
}
