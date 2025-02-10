using PortfolioApp.Core.DTOs.Email;

namespace PortfolioApp.Core.Interfaces;
public interface IEmailService
{
    Task<bool> SendEmailAsync(EmailRequestDto emailRequest);
}
