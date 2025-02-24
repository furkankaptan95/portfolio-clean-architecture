using MediatR;
using Microsoft.Extensions.Options;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Email;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Interfaces.Repositories;
using System.Net.Http.Json;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class NewVerificationHandler : IRequestHandler<NewVerificationCommand, ServiceResult>
{
    private readonly IUserRepository _userRepository;
    private readonly IUserVerificationRepository _userVerificationRepository;
    private readonly MVCLinksConfiguration _mVCLinksConfiguration;
    private readonly IHttpClientFactory _factory;
    public NewVerificationHandler(IHttpClientFactory factory, IOptions<MVCLinksConfiguration> mVCLinksConfiguration, IUserRepository userRepository, IUserVerificationRepository userVerificationRepository)
    {
        _mVCLinksConfiguration = mVCLinksConfiguration.Value;
        _factory = factory;
        _userRepository = userRepository;
        _userVerificationRepository = userVerificationRepository;
    }
    private HttpClient EmailApiClient => _factory.CreateClient("emailApi");
    public async Task<ServiceResult> Handle(NewVerificationCommand request, CancellationToken cancellationToken)
    {

        var user = await _userRepository.GetByEmailAsync(request.NewVerificationMail.Email);

        if (user is null || user.Role is not "User")
        {
            return new ServiceResult(false, "Bu Email adresine sahip bir kullanıcı bulunamadı.");
        }

        var token = Guid.NewGuid().ToString().Substring(0, 6);

        var userVerificationEntity = new UserVerificationEntity
        {
            UserId = user.Id,
            Token = token,
        };

        await _userVerificationRepository.AddAsync(userVerificationEntity);
        await _userVerificationRepository.SaveChangesAsync();

        string mvcLink;

        if (user.Role == "Admin")
            mvcLink = _mVCLinksConfiguration.Admin;
        else
            mvcLink = _mVCLinksConfiguration.Web;

        var verificationLink = $"{mvcLink}/Auth/VerifyEmail?email={request.NewVerificationMail.Email}&token={token}";

        var htmlMailBody = $"<h1>Lütfen Email adresinizi doğrulayın!</h1><a href='{verificationLink}'>Hesabınızı aktif etmek için tıklayınız.</a>";

        var emailRequest = new EmailRequestDto
        {
            Body = htmlMailBody,
            Subject = "Lütfen email adresinizi doğrulayın.",
            To = request.NewVerificationMail.Email,
        };

        var emailResult = await EmailApiClient.PostAsJsonAsync("send", emailRequest);

        if (emailResult.IsSuccessStatusCode)
            return new ServiceResult(true, "Lütfen hesabınızı aktif etmek için Email hesabınızı kontrol edin.");

        return new ServiceResult(false, "Bir hata oluştu.");
    }
}
