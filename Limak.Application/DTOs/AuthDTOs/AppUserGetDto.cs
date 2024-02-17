namespace Limak.Application.DTOs.AuthDTOs;

public class AppUserGetDto
{
    public int Id { get; set; }
    public string UserName { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string Birtday { get; set; } = null!;
    public string Fincode { get; set; } = null!;
    public string SeriaNumber { get; set; } = null!;
    public int GenderId { get; set; }
    public string Location { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public string Email { get; set; } = null!;
}
