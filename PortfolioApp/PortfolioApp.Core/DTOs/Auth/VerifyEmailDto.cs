namespace PortfolioApp.Core.DTOs.Auth;
public class VerifyEmailDto
{
    public string Token { get; set; }
    public string Email { get; set; }
    public VerifyEmailDto(string email,string token)
    {
        Email = email;
        Token = token;
    }
}
