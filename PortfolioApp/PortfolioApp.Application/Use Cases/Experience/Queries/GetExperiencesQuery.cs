using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.Application.Use_Cases.Experience.Queries;
public class GetExperiencesQuery : IRequest<ServiceResult<List<ExperienceDto>>> { }

