namespace Limak.Application.DTOs.ShopDTOs;

public record ShopGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImagePath{ get; set; }
    public string WebsitePath { get; set; }

}
