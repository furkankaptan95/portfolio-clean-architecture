using Microsoft.AspNetCore.Http;

namespace PortfolioApp.Core.DTOs.Admin.AboutMe;
public class AddAboutMeMvcDto
{
    public string Introduction { get; set; }
    public string FullName { get; set; }
    public string Field { get; set; }
    public string LinkedInUrl { get; set; }
    public string GithubUrl { get; set; }
    public string Email { get; set; }
    public IFormFile CvFile { get; set; }
    public IFormFile AboutMeImage { get; set; }
}
