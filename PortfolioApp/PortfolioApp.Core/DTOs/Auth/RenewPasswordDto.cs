namespace PortfolioApp.Core.DTOs.Auth;
public class RenewPasswordDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public RenewPasswordDto(string email , string token)
    {
        Email = email;
        Token = token;
    }
}
