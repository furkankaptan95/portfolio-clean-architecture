using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Enums;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services;
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

	public async Task<ServiceResult<RegistrationError>> RegisterAsync(RegisterDto dto)
	{
        var response = await AuthApiClient.PostAsJsonAsync("register", dto);

        return await response.Content.ReadFromJsonAsync<ServiceResult<RegistrationError>>();
    }

    public Task<ServiceResult<string>> RenewPasswordVerifyEmailAsync(RenewPasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> RevokeTokenAsync(string token)
	{
		throw new NotImplementedException();
	}

    public async Task<ServiceResult> VerifyEmailAsync(VerifyEmailDto dto)
    {
		var response = await AuthApiClient.PostAsJsonAsync("verify-email", dto);

		return await response.Content.ReadFromJsonAsync<ServiceResult>();
	}
    public Task<ServiceResult> NewPasswordAsync(NewPasswordDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> NewVerificationAsync(NewVerificationMailDto dto)
    {
        throw new NotImplementedException();
    }
}
