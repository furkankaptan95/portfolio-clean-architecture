using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Queries;
public class GetAboutMeQuery : IRequest<ServiceResult<AboutMeDto>> { }
