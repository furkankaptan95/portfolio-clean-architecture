using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.Application.Use_Cases.Education.Queries;
public class GetEducationsQuery : IRequest<ServiceResult<List<EducationDto>>> { }

