namespace PortfolioApp.WebMVC.Models.ViewModels;
public class AllBlogPostsViewModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public int CommentsCount { get; set; }
    public bool IsVisible { get; set; }
}
