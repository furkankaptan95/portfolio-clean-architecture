using MediatR;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class RenewPasswordVerifyEmailHandler : IRequestHandler<RenewPasswordVerifyEmailCommand, ServiceResult<string>>
{
    private readonly IUserVerificationRepository _userVerificationRepository;
    public RenewPasswordVerifyEmailHandler(IUserVerificationRepository userVerificationRepository)
    {
        _userVerificationRepository = userVerificationRepository;
    }
    public async Task<ServiceResult<string>> Handle(RenewPasswordVerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userVerification = await _userVerificationRepository.GetByTokenWithUser(request.RenewPassword.Token);

        if (userVerification == null || userVerification.ExpireDate < DateTime.UtcNow || userVerification.User.Email != request.RenewPassword.Email)
        {
            return new ServiceResult<string>(false, "Email onayı başarısız.");
        }

        return new ServiceResult<string>(true, "Email onayı başarılı. Şifrenizi yenileyebilirsiniz.", request.RenewPassword.Token);
    }
}
