namespace Limak.Application.DTOs.DeliveryDTOs;

public class DeliveryRelationDto
{
    public string Region { get; set; } = null!;
    public string Village { get; set; } = null!;
    public string Street { get; set; } = null!;
    public string HomeNo { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public string Notes { get; set; } = null!;
    public bool IsCancel { get; set; } = false;
    public int DeliveryAreaId { get; set; }
}
