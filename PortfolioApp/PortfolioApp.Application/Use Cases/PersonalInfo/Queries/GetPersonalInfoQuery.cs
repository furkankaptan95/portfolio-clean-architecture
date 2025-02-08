using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;

namespace PortfolioApp.Application.Use_Cases.PersonalInfo.Queries;
public class GetPersonalInfoQuery : IRequest<ServiceResult<PersonalInfoDto>> { }

