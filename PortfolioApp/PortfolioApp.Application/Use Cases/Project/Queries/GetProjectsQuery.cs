using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;

namespace PortfolioApp.Application.Use_Cases.Project.Queries;
public class GetProjectsQuery : IRequest<ServiceResult<List<ProjectDto>>> { }

