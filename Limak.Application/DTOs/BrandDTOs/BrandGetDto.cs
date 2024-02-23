namespace Limak.Application.DTOs.BrandDTOs;

public class BrandGetDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; } = null!;
    public string WebsitePath { get; set; } = null!;
}
