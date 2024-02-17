namespace Limak.Application.DTOs.AuthDTOs;

public class AppUserAccountDataPutDto
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public int WarehouseId { get; set; }
    public DateTime Birtday { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
}
