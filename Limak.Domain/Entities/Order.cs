using Limak.Domain.Entities.Common;

namespace Limak.Domain.Entities;

public class Order : BaseAuditableEntity
{
    public string OrderURL { get; set; } = null!;
    public decimal Price { get; set; }
    public decimal LocalCargoPrice { get; set; } = 0;
    public decimal CargoPrice { get; set; }
    public decimal Weight { get; set; }
    public bool OrderPaymentStatus { get; set; } = false;
    public bool CargoPaymentStatus { get; set; } = false;
    public int Count { get; set; } = 1;
    public string Color { get; set; } = null!;
    public string Size { get; set; } = null!;
    public string Notes { get; set; } = null!;
    public decimal AdditionFees { get; set; } = 0;
    public string? AdditionFeesNotes { get; set; }
    public decimal OrderTotalPrice { get; set; }
    public decimal TotalPrice { get; set; } = 0;    
    public bool IsCancel { get; set; } = false;
    public string? CancellationNotes { get; set; }
    //
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; } = null!;

    public int? ShopId { get; set; }
    public Shop? Shop { get; set; }

    public int? DeliveryId { get; set; }
    public Delivery? Delivery { get; set; }

    public int WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = null!;

    public int? KargomatId { get; set; }
    public Kargomat? Kargomat { get; set; }

    public int StatusId { get; set; }
    public Status Status { get; set; } = null!;

    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;




}
