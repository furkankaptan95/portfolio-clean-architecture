using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Home;

namespace PortfolioApp.Application.Use_Cases.Home.Queries;
public class GetHomeInfosQuery : IRequest<ServiceResult<HomeDto>> { }

