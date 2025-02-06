namespace PortfolioApp.Core.Entities;
public class AboutMeEntity : BaseEntity<int>
{
    public string Introduction { get; set; }
    public string FullName { get; set; }
    public string Field { get; set; }
    public string AboutMeImageUrl { get; set; }
    public string LinkedInUrl { get; set; }
    public string GithubUrl { get; set; }
    public string Email { get; set; }
    public string CvUrl { get; set; }
}
