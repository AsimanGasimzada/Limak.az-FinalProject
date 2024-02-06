namespace Limak.Application.DTOs.DeliveryAreaDTOs;

public class DeliveryAreaPutDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int WarehouseId { get; set; }
}
