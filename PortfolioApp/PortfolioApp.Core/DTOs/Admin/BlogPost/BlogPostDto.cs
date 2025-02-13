namespace PortfolioApp.Core.DTOs.Admin.BlogPost;
public class BlogPostDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsVisible { get; set; }
    public int CommentsCount { get; set; }
}
