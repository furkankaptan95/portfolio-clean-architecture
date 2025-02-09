using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Enums;

namespace PortfolioApp.Core.Interfaces;
public interface IAuthService
{
    Task<ServiceResult<RegistrationError>> RegisterAsync(RegisterDto dto);
    Task<ServiceResult<TokensDto>> LoginAsync(LoginDto loginDto);
}
