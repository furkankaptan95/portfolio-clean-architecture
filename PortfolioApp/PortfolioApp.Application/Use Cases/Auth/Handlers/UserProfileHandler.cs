using MediatR;
using PortfolioApp.Application.Use_Cases.Auth.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.User;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class UserProfileHandler : IRequestHandler<UserProfileQuery, ServiceResult<UserProfileDto>>
{
    private readonly IUserRepository _userRepository;
    public UserProfileHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<ServiceResult<UserProfileDto>> Handle(UserProfileQuery request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);

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
