using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class VerifyEmailCommand : IRequest<ServiceResult>
{
    public VerifyEmailDto Verify { get; set; }

    public VerifyEmailCommand(VerifyEmailDto verify)
    {
        Verify = verify;
    }
}
