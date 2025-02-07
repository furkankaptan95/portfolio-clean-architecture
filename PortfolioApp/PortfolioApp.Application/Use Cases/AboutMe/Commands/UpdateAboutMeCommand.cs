using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Commands;
public class UpdateAboutMeCommand : IRequest<ServiceResult>
{
    public UpdateAboutMeApiDto AboutMe { get; set; }
    public UpdateAboutMeCommand(UpdateAboutMeApiDto aboutMe)
    {
        AboutMe = aboutMe;
    }
}
