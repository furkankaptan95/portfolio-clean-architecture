namespace PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
public class UpdateAboutMeViewModel
{
    public string Introduction { get; set; }
    public string FullName { get; set; }
    public string Field { get; set; }
    public string LinkedInUrl { get; set; }
    public string GithubUrl { get; set; }
    public string Email { get; set; }
    public string CvUrl { get; set; }
    public string AboutMeImageUrl { get; set; }
    public IFormFile? CvFile { get; set; }
    public IFormFile? AboutMeImage { get; set; }
}
