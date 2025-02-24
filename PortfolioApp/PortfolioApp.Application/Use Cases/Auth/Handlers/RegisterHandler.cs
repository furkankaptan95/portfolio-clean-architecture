using MediatR;
using Microsoft.Extensions.Options;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Email;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Enums;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Core.Interfaces.Repositories;
using System.Net.Http.Json;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class RegisterHandler : IRequestHandler<RegisterCommand, ServiceResult<RegistrationError>>
{
    private readonly MVCLinksConfiguration _mVCLinksConfiguration;
    private readonly IHttpClientFactory _factory;
    private readonly IUserRepository _userRepository;
    private readonly IUserVerificationRepository _userVerificationRepository;
    public RegisterHandler(IHttpClientFactory factory,IOptions<MVCLinksConfiguration> mVCLinksConfiguration, IUserRepository userRepository, IUserVerificationRepository userVerificationRepository)
    {
        _mVCLinksConfiguration = mVCLinksConfiguration.Value;
        _factory = factory;
        _userRepository = userRepository;
        _userVerificationRepository = userVerificationRepository;
    }
    private HttpClient EmailApiClient => _factory.CreateClient("emailApi");
    public async Task<ServiceResult<RegistrationError>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var isEmailAlreadyTaken = await _userRepository.GetByEmailAsync(request.Register.Email);
        var isUsernameAlreadyTaken = await _userRepository.GetByUsernameAsync(request.Register.Username);

        if (isEmailAlreadyTaken is not null && isUsernameAlreadyTaken is not null)
        {
            return new ServiceResult<RegistrationError>(false, "Bu Email ve Kullanıcı Adı zaten daha önce alınmış!", RegistrationError.BothTaken);
        }

        else if (isEmailAlreadyTaken is null && isUsernameAlreadyTaken is not null)
        {
            return new ServiceResult<RegistrationError>(false, "Bu Kullanıcı Adı zaten daha önce alınmış!", RegistrationError.UsernameTaken);
        }

        else if (isEmailAlreadyTaken is not null && isUsernameAlreadyTaken is null)
        {
            return new ServiceResult<RegistrationError>(false, "Bu Email zaten daha önce alınmış!", RegistrationError.EmailTaken);
        }
        byte[] passwordHash, passwordSalt;

        HashingHelper.CreatePasswordHash(request.Register.Password, out passwordHash, out passwordSalt);

        var user = new UserEntity
        {
            Username = request.Register.Username,
            Email = request.Register.Email,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            Firstname = request.Register.Firstname,
            Lastname = request.Register.Lastname,
            
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();

        var token = Guid.NewGuid().ToString().Substring(0, 6);

        var userVerification = new UserVerificationEntity
        {
            UserId = user.Id,
            Token = token
        };

        await _userVerificationRepository.AddAsync(userVerification);
        await _userVerificationRepository.SaveChangesAsync();

        var mvcLink = _mVCLinksConfiguration.Web;

        var verificationLink = $"{mvcLink}/Auth/VerifyEmail?email={request.Register.Email}&token={token}";

        var htmlMailBody = $"<h1>Lütfen Email adresinizi doğrulayın!</h1><a href='{verificationLink}'>Hesabınızı aktif etmek için tıklayınız.</a>";

        var emailRequest = new EmailRequestDto
        {
            Body = htmlMailBody,
            Subject = "Lütfen email adresinizi doğrulayın.",
            To = request.Register.Email,
        };

        var emailResult = await EmailApiClient.PostAsJsonAsync("send", emailRequest);

        if (emailResult.IsSuccessStatusCode)
            return new ServiceResult<RegistrationError>(true, "Kayıt başarılı. Lütfen hesabı aktif etmek için Email hesabınızı kontrol edin.", RegistrationError.None);

        return new ServiceResult<RegistrationError>(false, "Kayıt başarılı ancak Onay Maili gönderilirken bir hata oluştu..", RegistrationError.None);
    }
}
