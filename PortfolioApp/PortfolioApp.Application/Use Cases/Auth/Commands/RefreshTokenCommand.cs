using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class RefreshTokenCommand : IRequest<ServiceResult<TokensDto>>
{
    public string Token { get; set; }

    public RefreshTokenCommand(string token)
    {
        Token = token;
    }
}
