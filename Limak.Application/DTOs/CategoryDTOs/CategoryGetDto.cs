namespace Limak.Application.DTOs.CategoryDTOs;

public record CategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }
    
}


public record CategoryRelationDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ImagePath { get; set; }

}

