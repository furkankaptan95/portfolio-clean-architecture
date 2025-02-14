using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Helpers;
public class ValidatePdfMimeTypeAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var file = value as IFormFile;
        if (file == null)
        {
            return ValidationResult.Success;
        }

        // Dosyanın MIME türünü kontrol et
        if (file.ContentType != "application/pdf")
        {
            return new ValidationResult("CV dosyası yalnızca PDF formatında olmalıdır.");
        }

        return ValidationResult.Success;
    }
}