﻿namespace PortfolioApp.Core.DTOs.Admin.Project;
public class ProjectDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public bool IsVisible { get; set; }
}
