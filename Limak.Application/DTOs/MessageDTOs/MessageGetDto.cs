namespace Limak.Application.DTOs.MessageDTOs;

public class MessageGetDto
{
    public int Id { get; set; }
    public int ChatId { get; set; }
    public string Body { get; set; } = null!;
    public string? FilePath { get; set; }
    public DateTime CreatedTime { get; set; }

}
