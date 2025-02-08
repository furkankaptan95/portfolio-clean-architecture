using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.Application.Use_Cases.Experience.Commands;
public class CreateExperienceCommand : IRequest<ServiceResult>
{
    public AddExperienceDto Experience { get; set; }
    public CreateExperienceCommand(AddExperienceDto experience)
    {
        Experience = experience;
    }
}

