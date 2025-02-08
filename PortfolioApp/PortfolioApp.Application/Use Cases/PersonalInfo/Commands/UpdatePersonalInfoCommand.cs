using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
public class UpdatePersonalInfoCommand : IRequest<ServiceResult>
{
    public UpdatePersonalInfoDto PersonalInfo { get; set; }
    public UpdatePersonalInfoCommand(UpdatePersonalInfoDto personalInfo)
    {
        PersonalInfo = personalInfo;
    }
}
