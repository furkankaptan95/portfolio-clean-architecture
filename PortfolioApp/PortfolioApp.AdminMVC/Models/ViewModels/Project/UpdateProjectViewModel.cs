namespace PortfolioApp.AdminMVC.Models.ViewModels.Project;
public class UpdateProjectViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string ImageUrl { get; set; }
}