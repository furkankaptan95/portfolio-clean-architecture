using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Enums;

namespace PortfolioApp.Core.Interfaces;
public interface IAuthService
{
    Task<ServiceResult<RegistrationError>> RegisterAsync(RegisterDto dto);
    Task<ServiceResult<TokensDto>> LoginAsync(LoginDto loginDto);
    Task<ServiceResult<TokensDto>> RefreshTokenAsync(string token);
    Task<ServiceResult> RevokeTokenAsync(string token);
    Task<ServiceResult> VerifyEmailAsync(VerifyEmailDto dto);
    Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto);
    Task<ServiceResult<string>> RenewPasswordVerifyEmailAsync(RenewPasswordDto dto);
}
