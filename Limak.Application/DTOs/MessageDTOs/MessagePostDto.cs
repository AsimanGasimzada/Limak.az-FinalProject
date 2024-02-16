using Microsoft.AspNetCore.Http;

namespace Limak.Application.DTOs.MessageDTOs;

public class MessagePostDto
{
    public int ChatId { get; set; }
    public string Body { get; set; } = null!;
    public IFormFile? File { get; set; }
}
