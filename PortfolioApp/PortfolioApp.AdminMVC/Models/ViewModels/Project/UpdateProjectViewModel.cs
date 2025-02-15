using PortfolioApp.AdminMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.Project;
public class UpdateProjectViewModel
{
    [Required(ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    public int Id { get; set; }
    public string ImageUrl { get; set; }

    [Required(ErrorMessage = "Başlık kısmı boş olamaz.")]
    [StringLength(100, ErrorMessage = "Başlık maksimum 100 karakter olabilir.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Açıklama kısmı boş olamaz.")]
    public string Description { get; set; }

    [ValidateImageMimeType(ErrorMessage = "Dosya yalnızca JPG, JPEG veya PNG formatında olmalıdır.")]
    public IFormFile? ImageFile { get; set; }
}