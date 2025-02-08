using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
public class CreatePersonalInfoCommand : IRequest<ServiceResult>
{
    public AddPersonalInfoDto PersonalInfo { get; set; }
    public CreatePersonalInfoCommand(AddPersonalInfoDto personalInfo)
    {
        PersonalInfo = personalInfo;
    }
}

