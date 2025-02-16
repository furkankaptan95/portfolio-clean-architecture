using MediatR;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Enums;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class AuthService : IAuthService
{
    private readonly IMediator _mediator;
    public AuthService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto)
    {
        var result = await _mediator.Send(new ForgotPasswordCommand(forgotPasswordDto));

        return result;
    }

    public async Task<ServiceResult<TokensDto>> LoginAsync(LoginDto loginDto)
    {
        var result = await _mediator.Send(new LoginCommand(loginDto));

        return result;
    }

    public async Task<ServiceResult<TokensDto>> RefreshTokenAsync(string token)
    {
        var result = await _mediator.Send(new RefreshTokenCommand(token));

        return result;
    }

    public async Task<ServiceResult<RegistrationError>> RegisterAsync(RegisterDto dto)
    {
        var result = await _mediator.Send(new RegisterCommand(dto));

        return result;
    }

    public async Task<ServiceResult> RevokeTokenAsync(string token)
    {
        var result = await _mediator.Send(new RevokeTokenCommand(token));

        return result;
    }

    public async Task<ServiceResult> VerifyEmailAsync(VerifyEmailDto dto)
    {
        var result = await _mediator.Send(new VerifyEmailCommand(dto));

        return result;
    }
}
