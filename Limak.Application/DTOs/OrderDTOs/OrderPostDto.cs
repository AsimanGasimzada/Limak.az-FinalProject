namespace Limak.Application.DTOs.OrderDTOs;

public class OrderPostDto
{
    public string OrderURL { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal LocalCargoPrice { get; set; } = 0;
    public int Count { get; set; } = 1;
    public string Color { get; set; } = null!;
    public string Size { get; set; } = null!;
    public string Notes { get; set; } = null!;
    public int WarehouseId { get; set; }
    public int CountryId { get; set; }
}



