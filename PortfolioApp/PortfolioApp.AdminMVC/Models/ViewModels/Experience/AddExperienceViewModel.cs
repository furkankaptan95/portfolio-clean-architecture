using PortfolioApp.AdminMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.Experience;
public class AddExperienceViewModel
{
    [Required(ErrorMessage = "Başlık kısmı boş olamaz.")]
    [StringLength(100, ErrorMessage = "Başlık maksimum 100 karakter olabilir.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "Şirket kısmı boş olamaz.")]
    [StringLength(100, ErrorMessage = "Şirket maksimum 100 karakter olabilir.")]
    public string Company { get; set; }

    [Required(ErrorMessage = "Açıklama kısmı boş olamaz.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Başlangıç tarihi gereklidir.")]
    [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih olmalı.")]
    public DateTime StartDate { get; set; }

    [DataType(DataType.Date, ErrorMessage = "Geçerli bir tarih olmalı.")]
    [EndDateAfterStartDate(nameof(StartDate), ErrorMessage = "Bitiş tarihi, başlangıç tarihinden sonra olmalı.")]
    public DateTime? EndDate { get; set; }
}
