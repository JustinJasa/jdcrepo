using Microsoft.AspNetCore.Http;
using System.IO;

namespace JasaDinnerClubBackend.Utils
{
    public static class ImageHelper
    {
        public static async Task<string> SaveImage(IFormFile? image, string uploadPath = "uploads")
        {
            if (image == null || image.Length == 0)
                return null; // No image uploaded

            // Ensure the uploads directory exists
            var uploadsDir = Path.Combine(Directory.GetCurrentDirectory(), uploadPath);
            if (!Directory.Exists(uploadsDir))
                Directory.CreateDirectory(uploadsDir);

            // Generate a unique filename to prevent conflicts
            var uniqueFileName = $"{Guid.NewGuid()}_{image.FileName}";
            var filePath = Path.Combine(uploadsDir, uniqueFileName);

            // Save the file
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            // Return the relative path for the client (served from /images)
            return Path.Combine("images", uniqueFileName).Replace("\\", "/");
        }
    }
}