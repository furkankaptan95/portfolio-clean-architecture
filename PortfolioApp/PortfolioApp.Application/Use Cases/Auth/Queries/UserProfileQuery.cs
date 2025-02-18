using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.User;

namespace PortfolioApp.Application.Use_Cases.Auth.Queries;
public class UserProfileQuery : IRequest<ServiceResult<UserProfileDto>>
{
    public int Id { get; }

    public UserProfileQuery(int id)
    {
        Id = id;
    }
}