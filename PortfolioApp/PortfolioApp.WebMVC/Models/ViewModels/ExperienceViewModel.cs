namespace PortfolioApp.WebMVC.Models.ViewModels;
public class ExperienceViewModel
{
    public string Title { get; set; } = "deneme";
    public string Company { get; set; } = "deneme";
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime? EndDate { get; set; } = DateTime.Now;
    public string Description { get; set; } = "deneme";
}
