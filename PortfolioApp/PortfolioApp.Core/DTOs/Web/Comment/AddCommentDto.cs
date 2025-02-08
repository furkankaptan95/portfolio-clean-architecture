namespace PortfolioApp.Core.DTOs.Web.Comment;
public class AddCommentDto
{
    public string Content { get; set; }
    public int BlogPostId { get; set; }
    public string? UnsignedCommenterName { get; set; }
    public int? UserId { get; set; }
    public string? SignedCommenterName { get; set; }
}
