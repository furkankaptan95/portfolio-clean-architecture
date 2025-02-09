using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Enums;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class RegisterHandler : IRequestHandler<RegisterCommand, ServiceResult<RegistrationError>>
{
    private readonly AuthDbContext _authDbContext;
    public RegisterHandler(AuthDbContext authDbContext)
    {
        _authDbContext = authDbContext;
    }
    public async Task<ServiceResult<RegistrationError>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isEmailAlreadyTaken = await _authDbContext.Users.SingleOrDefaultAsync(u => u.Email == request.Register.Email);
        var isUsernameAlreadyTaken = await _authDbContext.Users.SingleOrDefaultAsync(u => u.Username == request.Register.Username);

        if (isEmailAlreadyTaken is not null && isUsernameAlreadyTaken is not null)
        {
            return new ServiceResult<RegistrationError>(false, null, RegistrationError.BothTaken);
        }

        else if (isEmailAlreadyTaken is null && isUsernameAlreadyTaken is not null)
        {
            return new ServiceResult<RegistrationError>(false, null, RegistrationError.UsernameTaken);
        }

        else if (isEmailAlreadyTaken is not null && isUsernameAlreadyTaken is null)
        {
            return new ServiceResult<RegistrationError>(false, null, RegistrationError.EmailTaken);
        }
        byte[] passwordHash, passwordSalt;

        HashingHelper.CreatePasswordHash(request.Register.Password, out passwordHash, out passwordSalt);

        var user = new UserEntity
        {
            Username = request.Register.Username,
            Email = request.Register.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

        await _authDbContext.Users.AddAsync(user);
        await _authDbContext.SaveChangesAsync();

        var token = Guid.NewGuid().ToString().Substring(0, 6);

        var userVerification = new UserVerificationEntity
        {
            UserId = user.Id,
            Token = token,
            ExpireDate = DateTime.UtcNow.AddHours(24),
            CreatedAt = DateTime.UtcNow
        };

        await _authDbContext.UserVerifications.AddAsync(userVerification);
        await _authDbContext.SaveChangesAsync();

        return new ServiceResult<RegistrationError>(true, null, RegistrationError.None);
    }
}
