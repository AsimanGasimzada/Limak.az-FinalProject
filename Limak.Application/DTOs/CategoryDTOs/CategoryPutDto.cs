using Microsoft.AspNetCore.Http;

namespace Limak.Application.DTOs.CategoryDTOs;

public record CategoryPutDto
{
    public int Id { get; set; }
    public string  Name { get; set; }
    public IFormFile? Image{ get; set; }
}
