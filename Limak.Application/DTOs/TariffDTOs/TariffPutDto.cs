namespace Limak.Application.DTOs.TariffDTOs;

public class TariffPutDto
{
    public int Id { get; set; }
    public decimal MinValue { get; set; }
    public decimal MaxValue { get; set; }
    public decimal Price { get; set; }
    public int CountryId { get; set; }
}
