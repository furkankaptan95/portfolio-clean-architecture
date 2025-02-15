using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.WebMVC.Models.ViewModels;
public class AddContactMessageViewModel
{
    [Required(ErrorMessage = "İsim kısmı boş olamaz.")]
    [StringLength(50, ErrorMessage = "İsim kısmı en fazla 50 karakter olabilir.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Email kısmı zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
    [StringLength(100, ErrorMessage = "Email kısmı en fazla 100 karakter olabilir.")]
    public string Email { get; set; }

    [StringLength(100, ErrorMessage = "Konu kısmı en fazla 100 karakter olabilir.")]
    [RegularExpression(@"^(?!\s*$).+", ErrorMessage = "Konu kısmı yalnızca boşluklardan oluşamaz.")]
    public string? Subject { get; set; }

    [Required(ErrorMessage = "Mesaj kısmı boş olamaz.")]
    public string Message { get; set; }
}
