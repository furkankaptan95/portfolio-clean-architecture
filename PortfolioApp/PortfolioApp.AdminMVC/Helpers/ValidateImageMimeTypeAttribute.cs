using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Helpers;
public class ValidateImageMimeTypeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file == null)
        {
            return ValidationResult.Success;
        }

        // MIME türlerini kontrol et
        var allowedMimeTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
        if (!allowedMimeTypes.Contains(file.ContentType))
        {
            return new ValidationResult("AboutMeImage yalnızca JPG, JPEG veya PNG formatında olmalıdır.");
        }

        return ValidationResult.Success;
    }
}