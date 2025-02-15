using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.WebMVC.Models.ViewModels;
public class AddCommentViewModel
{
    public int? UserId { get; set; }

    [Required(ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    public int BlogPostId { get; set; }

    [StringLength(30, ErrorMessage = "Yorumcu adı maksimum 30 karakter olabilir.")]
    public string? UnsignedCommenterName { get; set; }

    [Required(ErrorMessage = "Yorum kısmı boş olamaz.")]
    [StringLength(300, ErrorMessage = "Yorum maksimum 300 karakter olabilir.")]
    public string Content { get; set; }
    public string? SignedCommenterName { get; set; }
}
