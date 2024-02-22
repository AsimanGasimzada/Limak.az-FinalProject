namespace Limak.Application.DTOs.OrderDTOs;

public class OrderAdminPutDto
{
    public int Id { get; set; }
    public decimal AdditionFees { get; set; } = 0;
    public string? AdditionFeesNotes { get; set; }
    public decimal Weight { get; set; }

}
