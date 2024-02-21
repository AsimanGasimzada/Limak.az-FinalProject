namespace Limak.Application.DTOs.DeliveryDTOs;

public class DeliveryGetDto
{
    public string Region { get; set; }
    public string Village { get; set; }
    public string Street { get; set; }
    public string HomeNo { get; set; }
    public string Phone { get; set; }
    public string Notes { get; set; }
    public bool IsCancel { get; set; } = false;
    public int DeliveryAreaId { get; set; }
}




public class DeliveryRelationDto
{
    public string Region { get; set; }
    public string Village { get; set; }
    public string Street { get; set; }
    public string HomeNo { get; set; }
    public string Phone { get; set; }
    public string Notes { get; set; }
    public bool IsCancel { get; set; } = false;
    public int DeliveryAreaId { get; set; }
}
