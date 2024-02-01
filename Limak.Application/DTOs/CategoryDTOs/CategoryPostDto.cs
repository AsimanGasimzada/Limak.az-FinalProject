using Microsoft.AspNetCore.Http;

namespace Limak.Application.DTOs.CategoryDTOs;

public record CategoryPostDto
{
    public string Name { get; set; }
    public IFormFile Image { get; set; }
}
