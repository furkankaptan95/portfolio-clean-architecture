using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class NewPasswordCommand : IRequest<ServiceResult>
{
    public NewPasswordDto NewPassword { get; set; }

    public NewPasswordCommand(NewPasswordDto newPassword)
    {
        NewPassword = newPassword;
    }
}
