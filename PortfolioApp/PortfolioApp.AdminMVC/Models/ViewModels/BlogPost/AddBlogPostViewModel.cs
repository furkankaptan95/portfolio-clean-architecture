using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
public class AddBlogPostViewModel
{
    [Required(ErrorMessage = "Başlık kısmı olamaz.")]
    [StringLength(100, ErrorMessage = "Başlık maksimum 100 karakter olabilir.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "İçerik kısmı boş olamaz.")]
    public string Content { get; set; }
}
