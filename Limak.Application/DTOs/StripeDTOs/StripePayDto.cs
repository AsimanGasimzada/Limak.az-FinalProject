namespace Limak.Application.DTOs.StripeDTOs;

public class StripePayDto
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string PhoneNumber { get; set; }
    public long Amount { get; set; }
    public string PublishableToken { get; set;}
}
