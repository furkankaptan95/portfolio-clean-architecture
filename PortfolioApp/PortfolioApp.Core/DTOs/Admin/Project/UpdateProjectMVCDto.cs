using Microsoft.AspNetCore.Http;

namespace PortfolioApp.Core.DTOs.Admin.Project;
public class UpdateProjectMVCDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile? ImageFile { get; set; }
    public string ImageUrl { get; set; }
}
