namespace Limak.Application.DTOs.StripeDTOs;

public class StripePayDto
{
    public string Email { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public decimal Amount { get; set; }
}
