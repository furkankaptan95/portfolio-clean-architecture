using MediatR;
using Microsoft.Extensions.Configuration;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Entities;
using PortfolioApp.Core.Helpers;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class LoginHandler : IRequestHandler<LoginCommand, ServiceResult<TokensDto>>
{
    private readonly IConfiguration _configuration;
    private readonly IUserRepository _userRepository;
    private readonly IRefreshTokenRespository _refreshTokenRespository;
    public LoginHandler(IConfiguration configuration, IUserRepository userRepository, IRefreshTokenRespository refreshTokenRespository)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _refreshTokenRespository = refreshTokenRespository;
    }
    public async Task<ServiceResult<TokensDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        UserEntity? user;

        if(request.Login.IsAdmin == true)
        {
            user = await _userRepository.GetByEmailWithRefreshTokensAsync(request.Login.Email);

            if (user is not null && user.Role is not "Admin")
            {
                user = null;
            }
        }

        else
        {
            user = await _userRepository.GetByEmailWithRefreshTokensAsync(request.Login.Email);

            if (user is not null && user.Role is not "User")
            {
                user = null;
            }
        }

        if (user == null)
        {
            return new ServiceResult<TokensDto>(false, "Hatalı kullanıcı adı veya şifre!");
        }

        if (!HashingHelper.VerifyPasswordHash(request.Login.Password, user.PasswordHash, user.PasswordSalt))
        {
            return new ServiceResult<TokensDto>(false, "Hatalı kullanıcı adı veya şifre!");
        }

        if (HashingHelper.VerifyPasswordHash(request.Login.Password, user.PasswordHash, user.PasswordSalt) && user.IsActive == false)
        {
            return new ServiceResult<TokensDto>(false, "Lütfen hesabınızı aktive edin.");
        }

        user.RefreshTokens.ToList().ForEach(t => t.IsRevoked = DateTime.UtcNow);

        string jwt = TokenGenerator.GenerateJwtToken(user, _configuration);

        string refreshTokenString = TokenGenerator.GenerateRefreshToken();

        var refreshToken = new RefreshTokenEntity
        {
            Token = refreshTokenString,
            UserId = user.Id,
            ExpireDate = DateTime.UtcNow.AddDays(7),
        };

        await _refreshTokenRespository.AddAsync(refreshToken);
        await _refreshTokenRespository.SaveChangesAsync();

        var tokensDto = new TokensDto
        {
            JwtToken = jwt,
            RefreshToken = refreshTokenString
        };

        return new ServiceResult<TokensDto>(true,"Giriş başarılı",tokensDto);
    }
}
