using PortfolioApp.AdminMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
public class UpdateAboutMeViewModel
{
    [Required(ErrorMessage = "Giriş kısmı boş olamaz.")]
    [StringLength(100, ErrorMessage = "Giriş maksimum 100 karakter olabilir.")]
    public string Introduction { get; set; }

    [Required(ErrorMessage = "Tam isim kısmı boş olamaz.")]
    [StringLength(50, ErrorMessage = "Tam isim maksimum 50 karakter olabilir.")]
    public string FullName { get; set; }

    [Required(ErrorMessage = "Alan kısmı boş olamaz.")]
    [StringLength(50, ErrorMessage = "Alan maksimum 50 karakter olabilir.")]
    public string Field { get; set; }

    [Required(ErrorMessage = "LinkedInUrl kısmı boş olamaz.")]
    [StringLength(255, ErrorMessage = "LinkedInUrl maksimum 255 karakter olabilir.")]
    public string LinkedInUrl { get; set; }

    [Required(ErrorMessage = "GithubUrl kısmı boş olamaz.")]
    [StringLength(255, ErrorMessage = "GithubUrl maksimum 255 karakter olabilir.")]
    public string GithubUrl { get; set; }

    [Required(ErrorMessage = "Email kısmı boş olamaz.")]
    [StringLength(100, ErrorMessage = "Email maksimum 100 karakter olabilir.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
    public string Email { get; set; }
    public string CvUrl { get; set; }
    public string AboutMeImageUrl { get; set; }

    [ValidatePdfMimeType(ErrorMessage = "CV dosyası yalnızca PDF formatında olmalıdır.")]
    public IFormFile? CvFile { get; set; }

    [ValidateImageMimeType(ErrorMessage = "AboutMeImage yalnızca JPG, JPEG veya PNG formatında olmalıdır.")]
    public IFormFile? AboutMeImage { get; set; }
}
