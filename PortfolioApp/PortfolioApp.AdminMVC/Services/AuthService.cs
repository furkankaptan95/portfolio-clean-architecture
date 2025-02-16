using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Enums;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class AuthService : IAuthService
{
    private readonly IHttpClientFactory _factory;
    public AuthService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient AuthApiClient => _factory.CreateClient("authApi");

    public Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<TokensDto>> LoginAsync(LoginDto loginDto)
    {
		var apiResponse = await AuthApiClient.PostAsJsonAsync("login", loginDto);

		return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<TokensDto>>();
	}

    public async Task<ServiceResult<TokensDto>> RefreshTokenAsync(string token)
    {
		var response = await AuthApiClient.PostAsJsonAsync("refresh-token", token);

		return await response.Content.ReadFromJsonAsync<ServiceResult<TokensDto>>();
	}

    public Task<ServiceResult<RegistrationError>> RegisterAsync(RegisterDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> RevokeTokenAsync(string token)
    {
	    var apiResponse = await AuthApiClient.PostAsJsonAsync("revoke-token", token);

		return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
	}

    public Task<ServiceResult> VerifyEmailAsync(VerifyEmailDto dto)
    {
        throw new NotImplementedException();
    }
}
