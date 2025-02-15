using PortfolioApp.AdminMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.Education;
public class AddEducationViewModel
{
    [Required(ErrorMessage = "Derece kısmı boş olamaz.")]
    [MaxLength(50, ErrorMessage = "Derece maksimum 50 karakter olabilir.")]
    public string Degree { get; set; }

    [Required(ErrorMessage = "Okul kısmı boş olamaz.")]
    [MaxLength(100, ErrorMessage = "Okul maksimum 100 karakter olabilir.")]
    public string School { get; set; }

    [Required(ErrorMessage = "Başlangıç tarihi gereklidir.")]
    [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih olmalı.")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih olmalı.")]
    [EndDateAfterStartDate(nameof(StartDate), ErrorMessage = "Bitiş tarihi, başlangıç tarihinden sonra olmalı.")]
    public DateTime? EndDate { get; set; }
}
