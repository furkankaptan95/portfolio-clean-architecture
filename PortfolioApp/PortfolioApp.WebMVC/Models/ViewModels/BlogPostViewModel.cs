namespace PortfolioApp.WebMVC.Models.ViewModels;
public class BlogPostViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public List<CommentViewModel> Comments { get; set; } = new();
}
