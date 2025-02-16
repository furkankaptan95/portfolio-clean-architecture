using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class NewVerificationCommand : IRequest<ServiceResult>
{
    public NewVerificationMailDto NewVerificationMail { get; set; }

    public NewVerificationCommand(NewVerificationMailDto newVerificationMail)
    {
        NewVerificationMail = newVerificationMail;
    }
}
