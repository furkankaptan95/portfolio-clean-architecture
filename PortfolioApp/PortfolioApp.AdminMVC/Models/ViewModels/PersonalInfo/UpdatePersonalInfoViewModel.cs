using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.PersonalInfo;
public class UpdatePersonalInfoViewModel
{
    [Required(ErrorMessage = "Hakkımda kısmı boş olamaz.")]
    [StringLength(300, ErrorMessage = "Hakkımda kısmı maksimum 300 karakter olabilir.")]
    public string About { get; set; }

    [Required(ErrorMessage = "İsim kısmı boş olamaz.")]
    [StringLength(50, ErrorMessage = "İsim maksimum 50 karakter olabilir.")]
    public string Name { get; set; }

    [Required(ErrorMessage = "Soyisim kısmı boş olamaz.")]
    [StringLength(50, ErrorMessage = "Soyisim maksimum 50 karakter olabilir.")]
    public string Surname { get; set; }

    [Required(ErrorMessage = "Adres kısmı boş olamaz.")]
    [StringLength(50, ErrorMessage = "Adres maksimum 50 karakter olabilir.")]
    public string Adress { get; set; }

    [Required(ErrorMessage = "Doğum tarihi gerekli.")]
    [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih giriniz.")]
    public DateTime BirthDate { get; set; }
}
