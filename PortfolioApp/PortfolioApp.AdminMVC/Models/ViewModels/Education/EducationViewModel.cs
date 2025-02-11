﻿namespace PortfolioApp.AdminMVC.Models.ViewModels.Education;
public class EducationViewModel
{
    public int Id { get; set; }
    public string Degree { get; set; }
    public string School { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public bool IsVisible { get; set; }
}
