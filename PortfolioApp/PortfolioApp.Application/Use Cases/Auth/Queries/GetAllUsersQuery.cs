using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.Application.Use_Cases.Auth.Queries;
public class GetAllUsersQuery :IRequest<ServiceResult<List<AllUsersDto>>>{}
