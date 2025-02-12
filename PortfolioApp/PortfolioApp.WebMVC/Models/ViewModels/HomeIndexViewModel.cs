namespace PortfolioApp.WebMVC.Models.ViewModels;
public class HomeIndexViewModel
{
    public AboutMeViewModel? AboutMe { get; set; }
    public List<EducationViewModel>? Educations { get; set; }
    public List<ExperienceViewModel>? Experiences { get; set; }
    public List<ProjectViewModel>? Projects { get; set; }
    public PersonalInfoViewModel? PersonalInfo { get; set; }
    public AddContactMessageViewModel ContactMessage { get; set; }
}
