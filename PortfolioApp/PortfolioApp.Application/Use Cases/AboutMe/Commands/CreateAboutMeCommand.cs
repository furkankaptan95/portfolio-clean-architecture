using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Commands;
public class CreateAboutMeCommand : IRequest<ServiceResult>
{
    public AddAboutMeApiDto AboutMe { get; set; }

    public CreateAboutMeCommand(AddAboutMeApiDto aboutMe)
    {
        AboutMe = aboutMe;
    }
}
