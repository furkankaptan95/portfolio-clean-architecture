using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;

public class RenewPasswordVerifyEmailCommand : IRequest<ServiceResult<string>>
{
    public RenewPasswordDto RenewPassword { get; set; }

    public RenewPasswordVerifyEmailCommand(RenewPasswordDto renewPassword)
    {
        RenewPassword = renewPassword;
    }
}
