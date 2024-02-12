namespace Limak.Application.DTOs.NewsDTOs;

public class NewsGetDto
{
    public int Id { get; set; }
    public string Subject { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public DateTime CreatedTime { get; set; }

}
