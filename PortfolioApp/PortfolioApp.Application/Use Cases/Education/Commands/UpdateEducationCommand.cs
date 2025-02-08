using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.Application.Use_Cases.Education.Commands;
public class UpdateEducationCommand : IRequest<ServiceResult>
{
    public UpdateEducationDto Education { get; set; }
    public UpdateEducationCommand(UpdateEducationDto education)
    {
        Education = education;
    }
}
