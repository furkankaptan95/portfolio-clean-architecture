using PortfolioApp.AdminMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.Project;
public class AddProjectViewModel
{
    [Required(ErrorMessage = "Başlık kısmı boş olamaz.")]
    [StringLength(100, ErrorMessage = "Başlık maksimum 100 karakter olabilir.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Açıklama kısmı boş olamaz.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Resim dosyası seçilmelidir.")]
    [ValidateImageMimeType(ErrorMessage = "Dosya yalnızca JPG, JPEG veya PNG formatında olmalıdır.")]
    public IFormFile ImageFile { get; set; }

}
