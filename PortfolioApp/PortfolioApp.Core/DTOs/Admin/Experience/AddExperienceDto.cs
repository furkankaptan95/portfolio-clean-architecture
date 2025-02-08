﻿namespace PortfolioApp.Core.DTOs.Admin.Experience;
public class AddExperienceDto
{
    public string Title { get; set; }
    public string Company { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
