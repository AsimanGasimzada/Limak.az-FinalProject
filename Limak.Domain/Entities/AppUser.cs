using Microsoft.AspNetCore.Identity;

namespace Limak.Domain.Entities;

public class AppUser:IdentityUser<int>
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string SeriaNumber { get; set; }
    public string FinCode{ get; set; }
    public DateTime Birtday { get; set; }
    public string Location { get; set; }
    public string Gender { get; set; }
    public decimal AZNBalance { get; set; }
    public decimal TRYBalance { get; set; }
    public decimal USDBalance { get; set; }

    public ICollection<Order> Orders { get; set; }

}
