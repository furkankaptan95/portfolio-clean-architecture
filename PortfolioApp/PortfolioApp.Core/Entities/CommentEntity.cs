namespace PortfolioApp.Core.Entities;
public class CommentEntity : BaseEntity<int>
{
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsApproved { get; set; } = false;
    public int? BlogPostId { get; set; }
    public BlogPostEntity? BlogPost { get; set; }
    public int? UserId { get; set; }
    public string? UnsignedCommenterName { get; set; }
    public string? SignedCommenterName { get; set; }

}
