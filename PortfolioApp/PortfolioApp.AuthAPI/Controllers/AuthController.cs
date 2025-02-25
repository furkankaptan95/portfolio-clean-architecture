using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AuthAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        var result = await _authService.RegisterAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var result = await _authService.LoginAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken([FromBody] string token)
    {
        var result = await _authService.RefreshTokenAsync(token);

        if(!result.IsSuccess)
            return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("revoke-token")]
    public async Task<IActionResult> RevokeToken([FromBody] string token)
    {
        var result = await _authService.RevokeTokenAsync(token);

        return Ok(result);
    }

    [HttpPost("verify-email")]
    public async Task<IActionResult> VerifyEmail([FromBody] VerifyEmailDto dto)
    {
        var result = await _authService.VerifyEmailAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
    {
        var result = await _authService.ForgotPasswordAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("renew-password-verify")]
    public async Task<IActionResult> RenewPassword([FromBody] RenewPasswordDto dto)
    {
        var result = await _authService.RenewPasswordVerifyEmailAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("new-password")]
    public async Task<IActionResult> NewPasswordAsync([FromBody] NewPasswordDto dto)
    {
        var result = await _authService.NewPasswordAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("new-verification")]
    public async Task<IActionResult> NewVerification([FromBody] NewVerificationMailDto dto)
    {
        var result = await _authService.NewVerificationAsync(dto);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin,User")]
    [HttpGet("user-profile/{userId}")]
    public async Task<IActionResult> UserProfile([FromRoute] int userId)
    {
        var result = await _authService.UserProfileAsync(userId);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [Authorize(Roles = "Admin")]
    [HttpGet("all-users")]
    public async Task<IActionResult> AllUsers()
    {
        var result = await _authService.GetAllUsersAsync();

        return Ok(result);
    }

}
