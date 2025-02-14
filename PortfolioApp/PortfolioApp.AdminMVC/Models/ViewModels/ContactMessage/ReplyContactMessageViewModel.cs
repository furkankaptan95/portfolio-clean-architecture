using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.ContactMessage;
public class ReplyContactMessageViewModel
{
    [Required(ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    public int Id { get; set; }

    [Required(ErrorMessage = "Mesaj kısmı boş olamaz.")]
    public string Message { get; set; }
}
