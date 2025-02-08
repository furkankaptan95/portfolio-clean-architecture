using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.Application.Use_Cases.Education.Commands;
public class CreateEducationCommand : IRequest<ServiceResult>
{
    public AddEducationDto Education { get; set; }
    public CreateEducationCommand(AddEducationDto dto)
    {
        Education = dto;
    }
}
