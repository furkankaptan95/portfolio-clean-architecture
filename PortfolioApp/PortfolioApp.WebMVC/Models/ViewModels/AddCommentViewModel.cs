namespace PortfolioApp.WebMVC.Models.ViewModels;
public class AddCommentViewModel
{
    public int? UserId { get; set; }
    public int BlogPostId { get; set; }
    public string? UnsignedCommenterName { get; set; }
    public string Content { get; set; }
    public string? SignedCommenterName { get; set; }
}
