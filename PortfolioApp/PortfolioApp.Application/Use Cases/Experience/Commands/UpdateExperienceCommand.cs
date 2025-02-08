using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.Application.Use_Cases.Experience.Commands;
public class UpdateExperienceCommand : IRequest<ServiceResult>
{
    public UpdateExperienceDto Experience { get; set; }
    public UpdateExperienceCommand(UpdateExperienceDto experience)
    {
        Experience = experience;
    }
}
