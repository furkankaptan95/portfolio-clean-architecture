namespace PortfolioApp.WebMVC.Models.ViewModels;
public class EducationViewModel
{
    public string Degree { get; set; } = "deneme";
    public string School { get; set; } = "deneme";
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = DateTime.Now;
}
