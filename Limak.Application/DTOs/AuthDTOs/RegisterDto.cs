namespace Limak.Application.DTOs.AuthDTOs;

public class RegisterDto
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string SeriaNumber { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public string FinCode { get; set; }
    public DateTime Birtday { get; set; }
    public string Location { get; set; }
    public int GenderId { get; set; }
    public int CitizenshipId { get; set; }
    public int UserPositionId { get; set; }
    public int WarehouseId { get; set; }


}
