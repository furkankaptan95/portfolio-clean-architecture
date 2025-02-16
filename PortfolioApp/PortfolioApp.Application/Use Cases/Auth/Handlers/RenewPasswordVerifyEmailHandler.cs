using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class RenewPasswordVerifyEmailHandler : IRequestHandler<RenewPasswordVerifyEmailCommand, ServiceResult<string>>
{
    private readonly AuthDbContext _authDbContext;
    public RenewPasswordVerifyEmailHandler(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    public async Task<ServiceResult<string>> Handle(RenewPasswordVerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userVerification = await _authDbContext.UserVerifications.Include(uv => uv.User).FirstOrDefaultAsync(uv => uv.Token == request.RenewPassword.Token);

        if (userVerification == null || userVerification.ExpireDate < DateTime.UtcNow || userVerification.User.Email != request.RenewPassword.Email)
        {
            return new ServiceResult<string>(false, "Email onayı başarısız.");
        }

        return new ServiceResult<string>(true, "Email onayı başarılı. Şifrenizi yenileyebilirsiniz.", request.RenewPassword.Token);
    }
}
