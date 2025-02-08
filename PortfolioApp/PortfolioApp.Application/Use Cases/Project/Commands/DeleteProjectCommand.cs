using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Project.Commands;
public class DeleteProjectCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public DeleteProjectCommand(int id)
    {
        Id = id;
    }
}
