using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class ForgotPasswordCommand : IRequest<ServiceResult>
{
    public ForgotPasswordDto ForgotPassword { get; set; }

    public ForgotPasswordCommand(ForgotPasswordDto forgotPassword)
    {
        ForgotPassword = forgotPassword;
    }
}
