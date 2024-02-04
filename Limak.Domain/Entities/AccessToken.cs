namespace Limak.Domain.Entities;

public class AccessToken
{
    public string Token { get; set; }
    public DateTime ExpiredDate { get; set; }

    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiredAt { get; set; }
}
