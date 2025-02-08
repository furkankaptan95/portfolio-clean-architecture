using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.Application.Use_Cases.Experience.Queries;
public class GetExperienceByIdQuery : IRequest<ServiceResult<ExperienceDto>>
{
    public int Id { get; }

    public GetExperienceByIdQuery(int id)
    {
        Id = id;
    }
}
