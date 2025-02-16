using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class NewPasswordHandler : IRequestHandler<NewPasswordCommand, ServiceResult>
{
    private readonly AuthDbContext _authDbContext;
    public NewPasswordHandler(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    public async Task<ServiceResult> Handle(NewPasswordCommand request, CancellationToken cancellationToken)
    {
        var userVerification = await _authDbContext.UserVerifications.Include(uv => uv.User).FirstOrDefaultAsync(uv => uv.Token == request.NewPassword.Token);

        if (userVerification == null || userVerification.ExpireDate < DateTime.UtcNow || userVerification.User.Email != request.NewPassword.Email)
        {
            return new ServiceResult(false, "Şifre yenileme başarısız.");
        }

        var user = userVerification.User;

        byte[] passwordHash, passwordSalt;

        HashingHelper.CreatePasswordHash(request.NewPassword.Password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _authDbContext.Users.Update(user);
        await _authDbContext.SaveChangesAsync(cancellationToken);

        _authDbContext.UserVerifications.Remove(userVerification);
        await _authDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Şifreniz başarıyla değiştirildi.");
    }
}
