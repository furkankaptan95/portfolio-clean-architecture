namespace PortfolioApp.Core.Entities;
public class UserVerificationEntity : BaseEntity<int>
{
    public int UserId { get; set; }
    public string Token { get; set; }
    public DateTime ExpireDate { get; set; } = DateTime.UtcNow.AddHours(24);
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual UserEntity User { get; set; }
}
