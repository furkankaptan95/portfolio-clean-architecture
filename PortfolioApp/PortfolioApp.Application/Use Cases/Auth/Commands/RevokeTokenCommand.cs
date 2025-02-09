using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class RevokeTokenCommand : IRequest<ServiceResult>
{
    public string Token { get; set; }

    public RevokeTokenCommand(string token)
    {
        Token = token;
    }
}
