using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Experience.Commands;
public class ExperienceVisibilityCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }

    public ExperienceVisibilityCommand(int id)
    {
        Id = id;
    }
}
