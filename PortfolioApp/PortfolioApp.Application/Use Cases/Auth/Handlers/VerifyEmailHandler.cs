using MediatR;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces.Repositories;
namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;

public class VerifyEmailHandler : IRequestHandler<VerifyEmailCommand, ServiceResult>
{
    private readonly IUserVerificationRepository _userVerificationRepository;
    private readonly IUserRepository _userRepository;
    public VerifyEmailHandler(IUserVerificationRepository userVerificationRepository, IUserRepository userRepository)
    {
        _userVerificationRepository = userVerificationRepository;
        _userRepository = userRepository;
    }
    public async Task<ServiceResult> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
    {
        var userVerification = await _userVerificationRepository.GetByTokenWithUser(request.Verify.Token);

        if (userVerification is null)
        {
            return new ServiceResult(false, "User Verification not found");
        }

        if (userVerification.ExpireDate < DateTime.UtcNow || userVerification.User.Email != request.Verify.Email)
        {
            return new ServiceResult(false, "No valid User Verification found");
        }

        userVerification.User.IsActive = true;

        await _userRepository.UpdateAsync(userVerification.User);
        await _userRepository.SaveChangesAsync();

        await _userVerificationRepository.DeleteAsync(userVerification);
        await _userVerificationRepository.SaveChangesAsync();

        return new ServiceResult(true, "Hesabınız başarıyla aktifleştirildi. Giriş yapabilirsiniz.");
    }
}
