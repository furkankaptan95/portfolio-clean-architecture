using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.User;
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

	public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
	{
		var apiResponse = await AuthApiClient.PostAsJsonAsync("forgot-password", forgotPasswordDto);

		return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
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

	public async Task<ServiceResult<string>> RenewPasswordVerifyEmailAsync(RenewPasswordDto dto)
	{
		var response = await AuthApiClient.PostAsJsonAsync("renew-password-verify", dto);

		return await response.Content.ReadFromJsonAsync<ServiceResult<string>>();
	}

	public async Task<ServiceResult> RevokeTokenAsync(string token)
	{
		var apiResponse = await AuthApiClient.PostAsJsonAsync("revoke-token", token);

		return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
	}

	public async Task<ServiceResult> VerifyEmailAsync(VerifyEmailDto dto)
    {
		var response = await AuthApiClient.PostAsJsonAsync("verify-email", dto);

		return await response.Content.ReadFromJsonAsync<ServiceResult>();
	}
	public async Task<ServiceResult> NewPasswordAsync(NewPasswordDto dto)
	{
		var apiResponse = await AuthApiClient.PostAsJsonAsync("new-password", dto);

		return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
	}

	public async Task<ServiceResult> NewVerificationAsync(NewVerificationMailDto dto)
    {
        var apiResponse = await AuthApiClient.PostAsJsonAsync("new-verification", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public async Task<ServiceResult<UserProfileDto>> UserProfileAsync(int userId)
    {
        var apiResponse = await AuthApiClient.GetAsync($"user-profile/{userId}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<UserProfileDto>>();
    }
}
