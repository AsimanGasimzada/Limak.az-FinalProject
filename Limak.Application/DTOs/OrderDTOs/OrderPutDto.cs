namespace Limak.Application.DTOs.OrderDTOs;

public class OrderPutDto
{
    public int Id { get; set; }
    public string OrderURL { get; set; }
    public decimal Price { get; set; }
    public decimal LocalCargoPrice { get; set; } = 0;
    public int Count { get; set; } = 1;
    public string Notes { get; set; }
}
