using PortfolioApp.Core.DTOs.Web.Comment;

namespace PortfolioApp.Core.DTOs.Web.BlogPost;
public class BlogPostWebDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime PublishDate { get; set; }
    public List<CommentWebDto>? Comments { get; set; } = new();
}
