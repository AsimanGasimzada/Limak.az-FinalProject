using Microsoft.AspNetCore.Http;

namespace Limak.Application.Abstractions.Services;

public interface ICloudinaryService
{
    Task<string> FileCreateAsync(IFormFile file);
    Task<bool> FileDeleteAsync(string filename);

}
