namespace PortfolioApp.Core.DTOs.Admin.Education;
public class AddEducationDto
{
    public string Degree { get; set; }
    public string School { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
}
