﻿namespace PortfolioApp.Core.DTOs.Admin.AboutMe;
public class UpdateAboutMeApiDto
{
    public string FullName { get; set; }
    public string Field { get; set; }
    public string Introduction { get; set; }
    public string? AboutMeImageUrl { get; set; }
    public string LinkedInUrl { get; set; }
    public string GithubUrl { get; set; }
    public string Email { get; set; }
    public string? CvUrl { get; set; }
}
