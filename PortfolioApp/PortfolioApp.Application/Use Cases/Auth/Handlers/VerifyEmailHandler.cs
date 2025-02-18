using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Infrastructure.Persistence.DbContexts;
namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;

public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, ServiceResult>
{
    private readonly AuthDbContext _authDbContext;
    public VerifyEmailHandler(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    public async Task<ServiceResult> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userVerification = await _authDbContext.UserVerifications.Include(uv=>uv.User).FirstOrDefaultAsync(uv => uv.Token == request.Verify.Token);

        if (userVerification is null)
        {
            return new ServiceResult(false, "User Verification not found");
        }

        if (userVerification.ExpireDate < DateTime.UtcNow || userVerification.User.Email != request.Verify.Email)
        {
            return new ServiceResult(false, "No valid User Verification found");
        }

        userVerification.User.IsActive = true;

        _authDbContext.Users.Update(userVerification.User);
        await _authDbContext.SaveChangesAsync(cancellationToken);

        _authDbContext.UserVerifications.Remove(userVerification);

        return new ServiceResult(true, "Hesabınız başarıyla aktifleştirildi. Giriş yapabilirsiniz.");
    }
}
