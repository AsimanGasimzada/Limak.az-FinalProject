namespace Limak.Application.DTOs.OrderDTOs;

public class OrderPostDto
{
    public string OrderURL { get; set; }
    public decimal Price { get; set; }
    public decimal LocalCargoPrice { get; set; } = 0;
    public int Count { get; set; } = 1;
    public string Color { get; set; }
    public string Size { get; set; }
    public string Notes { get; set; }
    public int WarehouseId { get; set; }
    public int CountryId { get; set; }


}
