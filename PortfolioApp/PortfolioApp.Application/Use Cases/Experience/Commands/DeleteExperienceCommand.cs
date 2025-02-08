using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Experience.Commands;
public class DeleteExperienceCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public DeleteExperienceCommand(int id)
    {
        Id = id;
    }
}
