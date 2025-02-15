using PortfolioApp.AdminMVC.Helpers;
using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.AdminMVC.Models.ViewModels.Experience;
public class UpdateExperienceViewModel
{
    [Required(ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    [Range(1, int.MaxValue, ErrorMessage = "Id 0'dan büyük olmalıdır.")]
    public int Id { get; set; }

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
