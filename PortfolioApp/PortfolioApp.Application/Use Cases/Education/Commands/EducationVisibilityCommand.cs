using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Education.Commands;
public class EducationVisibilityCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }

    public EducationVisibilityCommand(int id)
    {
        Id = id;
    }
}
