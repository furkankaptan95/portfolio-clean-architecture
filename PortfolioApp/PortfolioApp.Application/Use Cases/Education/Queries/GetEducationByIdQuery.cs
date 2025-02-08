using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.Application.Use_Cases.Education.Queries;
public class GetEducationByIdQuery : IRequest<ServiceResult<EducationDto>>
{
    public int Id { get; }

    public GetEducationByIdQuery(int id)
    {
        Id = id;
    }
}
