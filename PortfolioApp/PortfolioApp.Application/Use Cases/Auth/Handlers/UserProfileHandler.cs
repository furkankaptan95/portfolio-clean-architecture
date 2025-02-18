using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Auth.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.User;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class UserProfileHandler : IRequestHandler<UserProfileQuery, ServiceResult<UserProfileDto>>
{
    private readonly AuthDbContext _authDbContext;
    public UserProfileHandler(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    public async Task<ServiceResult<UserProfileDto>> Handle(UserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _authDbContext.Users.SingleOrDefaultAsync(u=>u.Id == request.Id);

        if (user is null)
            return new ServiceResult<UserProfileDto>(false,"Kullanıcı bulunamadı.");

        var dto = new UserProfileDto
        {           
            Firstname = user.Firstname,
            Lastname = user.Lastname,
            Username = user.Username,
            Email = user.Email,
        };

        return new ServiceResult<UserProfileDto>(true,null,dto);
    }
}
