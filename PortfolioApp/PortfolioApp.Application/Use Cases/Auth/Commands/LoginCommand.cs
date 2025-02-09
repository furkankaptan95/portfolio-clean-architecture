using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class LoginCommand : IRequest<ServiceResult<TokensDto>>
{
    public LoginDto Login { get; set; }

    public LoginCommand(LoginDto login)
    {
        Login = login;
    }
}
