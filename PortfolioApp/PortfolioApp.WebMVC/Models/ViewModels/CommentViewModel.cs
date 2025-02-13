namespace PortfolioApp.WebMVC.Models.ViewModels;
public class CommentViewModel
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public string Commenter { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
}
