namespace PortfolioApp.Core.Entities;
public class SocialLinksEntity : BaseEntity<int>
{
    public string? TwitterUrl { get; set; }
    public string? MediumUrl { get; set; }
    public string? InstagramUrl { get; set; }
    public string? GithubUrl { get; set; }
    public string? LinkedInUrl { get; set; }
}
