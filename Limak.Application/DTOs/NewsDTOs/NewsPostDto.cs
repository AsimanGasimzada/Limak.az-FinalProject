using Microsoft.AspNetCore.Http;

namespace Limak.Application.DTOs.NewsDTOs;

public class NewsPostDto
{
    public string Subject { get; set; }
    public string Description { get; set; }
    public IFormFile Image { get; set; }
}
