using Limak.Domain.Entities;

namespace Limak.Application.DTOs.DeliveryDTOs;

public class DeliveryPostDto
{
    public int DeliveryAreaId { get; set; }
    public string Region { get; set; }
    public string Village { get; set; }
    public string Street { get; set; }
    public string HomeNo { get; set; }
    public string Phone { get; set; }
    public string Notes { get; set; }
    public List<int> OrderIds { get; set; }
}
