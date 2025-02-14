using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
public class UpdateBlogPostViewModel
{
    [Required(ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Başlık kısmı boş olamaz.")]
    [StringLength(100, ErrorMessage = "Başlık maksimum 100 karakter olabilir.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "İçerik kısmı boş olamaz.")]
    public string Content { get; set; }
}
