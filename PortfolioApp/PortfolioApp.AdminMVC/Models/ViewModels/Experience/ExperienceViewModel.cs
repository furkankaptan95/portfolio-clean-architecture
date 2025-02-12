﻿namespace PortfolioApp.AdminMVC.Models.ViewModels.Experience;
public class ExperienceViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Company { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public string Description { get; set; }
    public bool IsVisible { get; set; }
}
