using Limak.Application.DTOs.OrderDTOs;

namespace Limak.Application.DTOs.KargomatDTOs;

public class KargomatGetDto
{

    public int Id { get; set; }
    public string Location { get; set; } = null!;
    public string CordinateX { get; set; } = null!;
    public string CordinateY { get; set; } = null!;
    public decimal Price { get; set; }
}
