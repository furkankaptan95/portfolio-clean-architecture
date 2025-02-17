using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.Auth;
public class ForgotPasswordViewModel
{
	[Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
	[EmailAddress(ErrorMessage = "Geçerli bir email adresi giriniz.")]
	[MaxLength(100, ErrorMessage = "Email 100 karakterden uzun olamaz.")]
	public string Email { get; set; }
}
