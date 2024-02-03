using Limak.Persistence.Utilities.Exceptions.Common;
using Microsoft.AspNetCore.Http;

namespace Limak.Persistence.Utilities.Helpers;

public static class FileValidator
{
    public static bool ValidateType(this IFormFile file, string type = "image")
    {
        if (!file.ContentType.Contains(type)) return false;

        return true;
    }
    public static bool ValidateSize(this IFormFile file, int mb)
    {
        if (file.Length > mb * 1024 * 1024) return false;

        return true;
    }
    public static void ValidateImage(this IFormFile file,int mb=2)
    {
        if (!file.ValidateType())
        {
            throw new InvalidInputException("Please enter a image");
        };
        if(!file.ValidateSize(mb))
        {
            throw new InvalidInputException($"The maximum size of the Image should be {mb} MB");
        }
    }
}
