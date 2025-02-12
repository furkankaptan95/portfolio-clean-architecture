using Microsoft.AspNetCore.Http;

namespace PortfolioApp.Core.DTOs.Admin.Project;
public class AddMvcProjectDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public IFormFile ImageFile { get; set; }
}
