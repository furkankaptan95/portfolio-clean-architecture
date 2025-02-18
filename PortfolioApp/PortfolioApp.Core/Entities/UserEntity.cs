namespace PortfolioApp.Core.Entities;
public class UserEntity : BaseEntity<int>
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; } = false;
    public byte[] PasswordHash { get; set; } = default!;
    public byte[] PasswordSalt { get; set; } = default!;
    public string Role { get; set; } = "User";
    public virtual ICollection<RefreshTokenEntity> RefreshTokens { get; set; } = default!;
    public virtual ICollection<UserVerificationEntity> UserVerifications { get; set; } = default!;

}
