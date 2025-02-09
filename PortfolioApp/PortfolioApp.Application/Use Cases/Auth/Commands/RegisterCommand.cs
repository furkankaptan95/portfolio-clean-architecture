using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Enums;

namespace PortfolioApp.Application.Use_Cases.Auth.Commands;
public class RegisterCommand : IRequest<ServiceResult<RegistrationError>>
{
    public RegisterDto Register { get; set; }

    public RegisterCommand(RegisterDto register)
    {
        Register = register;
    }
}
