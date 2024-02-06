using Limak.Domain.Entities;

namespace Limak.Application.DTOs.OrderDTOs;

public class OrderGetDto
{
    public int Id { get; set; }
    public string OrderURL { get; set; }
    public decimal Price { get; set; }
    public decimal LocalCargoPrice { get; set; } = 0;
    public decimal CargoPrice { get; set; }
    public decimal Weight { get; set; }
    public bool OrderPaymentStatus { get; set; } = false;
    public bool CargoPaymentStatus { get; set; } = false;
    public int Count { get; set; } = 1;
    public string Color { get; set; }
    public string Size { get; set; }
    public string Notes { get; set; }
    public decimal AdditionFees { get; set; } = 0;
    public string AdditionFeesNotes { get; set; }
    //
    public int AppUserId { get; set; }

    public int? ShopId { get; set; }

    public int? DeliveryId { get; set; }

    public int WarehouseId { get; set; }

    public int? KargomatId { get; set; }

    public int StatusId { get; set; }

    public int CountryId { get; set; }

}
