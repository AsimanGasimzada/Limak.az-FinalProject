namespace Limak.Application.DTOs.CategoryDTOs;

public record CategoryRelationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string ImagePath { get; set; } = null!;

}

