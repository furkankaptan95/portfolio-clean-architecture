using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Email;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;
using System.Net.Http.Json;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class ForgotPasswordHandler : IRequestHandler<ForgotPasswordCommand, ServiceResult>
{
    private readonly AuthDbContext _authDbContext;
    private readonly MVCLinksConfiguration _mVCLinksConfiguration;
    private readonly IHttpClientFactory _factory;
    public ForgotPasswordHandler(IHttpClientFactory factory, AuthDbContext authDbContext, IOptions<MVCLinksConfiguration> mVCLinksConfiguration)
    {
        _authDbContext = authDbContext;
        _mVCLinksConfiguration = mVCLinksConfiguration.Value;      
        _factory = factory;
    }
    private HttpClient EmailApiClient => _factory.CreateClient("emailApi");
    public async Task<ServiceResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        UserEntity? user;

        if (request.ForgotPassword.IsAdmin == true)
        {
            user = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.ForgotPassword.Email && u.Role == "Admin");
        }
        else
        {
            user = await _authDbContext.Users.FirstOrDefaultAsync(u => u.Email == request.ForgotPassword.Email && u.Role == "User");
        }

        if (user is null)
        {
            return new ServiceResult(false, "Bu Email adresine sahip bir kullanıcı bulunamadı.");
        }

        var token = Guid.NewGuid().ToString().Substring(0, 6);

        var userVerificationEntity = new UserVerificationEntity
        {
            UserId = user.Id,
            Token = token,
        };

        await _authDbContext.UserVerifications.AddAsync(userVerificationEntity);
        await _authDbContext.SaveChangesAsync();

        string mvcLink;

        if (user.Role == "Admin")
            mvcLink = _mVCLinksConfiguration.Admin;
        else
            mvcLink = _mVCLinksConfiguration.Web;

        var verificationLink = $"{mvcLink}/Auth/RenewPassword?email={request.ForgotPassword.Email}&token={token}";

        var htmlMailBody = $"<h1>Lütfen Email adresinizi doğrulayın!</h1><a href='{verificationLink}'>Şifrenizi sıfırlamak için tıklayınız.</a>";

        var emailRequest = new EmailRequestDto
        {
            Body = htmlMailBody,
            Subject = "Lütfen email adresinizi doğrulayın.",
            To = request.ForgotPassword.Email,
        };

        var emailResult = await EmailApiClient.PostAsJsonAsync("send", emailRequest);

        if (emailResult.IsSuccessStatusCode)
            return new ServiceResult(true, "Lütfen şifrenizi yenilemek için Email adresinizi ziyaret edin.");

        return new ServiceResult(false, "Bir hata oluştu.");
    }
}

