namespace PortfolioApp.Core.DTOs.Auth;
public class AllUsersDto
{
    public string Firstname { get; set; }
    public string Lastname { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Role { get; set; }
    public bool IsActive { get; set; }
}
