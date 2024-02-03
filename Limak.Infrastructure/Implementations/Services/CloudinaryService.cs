using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Limak.Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Limak.Infrastructure.Implementations.Services;

public class CloudinaryService:ICloudinaryService
{
    private readonly IConfiguration _configuration;

    public CloudinaryService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<string> FileCreateAsync(IFormFile file)
    {
        string fileName = string.Concat(Guid.NewGuid(), file.FileName.Substring(file.FileName.LastIndexOf('.')));
        var myAccount = new Account { ApiKey = _configuration["CloudinarySettings:APIKey"], ApiSecret = _configuration["CloudinarySettings:APISecret"], Cloud = _configuration["CloudinarySettings:CloudName"] };

        Cloudinary _cloudinary = new Cloudinary(myAccount);
        _cloudinary.Api.Secure = true;
        var uploadResult = new ImageUploadResult();
        if (file.Length > 0)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, stream),
            };
            uploadResult = await _cloudinary.UploadAsync(uploadParams);
        }
        string url = uploadResult.SecureUri.ToString();
        return url;
    }
}
