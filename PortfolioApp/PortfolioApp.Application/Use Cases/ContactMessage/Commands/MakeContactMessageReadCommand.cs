using MediatR;
using PortfolioApp.Core.Common;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
public class MakeContactMessageReadCommand : IRequest<ServiceResult>
{
    public int Id { get; set; }

    public MakeContactMessageReadCommand(int id)
    {
        Id = id;
    }
}
