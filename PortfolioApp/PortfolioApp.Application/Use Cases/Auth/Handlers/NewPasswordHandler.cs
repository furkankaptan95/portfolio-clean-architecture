using MediatR;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class NewPasswordHandler : IRequestHandler<NewPasswordCommand, ServiceResult>
{
    private readonly IUserVerificationRepository _userVerificationRepository;
    private readonly IUserRepository _userRepository;
    public NewPasswordHandler(IUserVerificationRepository userVerificationRepository, IUserRepository userRepository)
    {
        _userVerificationRepository = userVerificationRepository;
        _userRepository = userRepository;
    }
    public async Task<ServiceResult> Handle(NewPasswordCommand request, CancellationToken cancellationToken)
    {
        var userVerification = await _userVerificationRepository.GetByTokenWithUser(request.NewPassword.Token);

        if (userVerification == null || userVerification.ExpireDate < DateTime.UtcNow || userVerification.User.Email != request.NewPassword.Email)
        {
            return new ServiceResult(false, "Şifre yenileme başarısız.");
        }

        var user = userVerification.User;

        byte[] passwordHash, passwordSalt;

        HashingHelper.CreatePasswordHash(request.NewPassword.Password, out passwordHash, out passwordSalt);

        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();

        await _userVerificationRepository.DeleteAsync(userVerification);
        await _userVerificationRepository.SaveChangesAsync();

        return new ServiceResult(true, "Şifreniz başarıyla değiştirildi.");
    }
}
