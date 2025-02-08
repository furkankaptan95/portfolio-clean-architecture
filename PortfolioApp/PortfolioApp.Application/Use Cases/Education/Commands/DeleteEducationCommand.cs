using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.Education.Commands;
public class DeleteEducationCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }
    public DeleteEducationCommand(int id)
    {
        Id = id;
    }
}
