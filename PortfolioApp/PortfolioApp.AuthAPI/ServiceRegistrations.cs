using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Mappers;
using PortfolioApp.Application.Use_Cases.Auth.Commands;
using PortfolioApp.Application.Use_Cases.Auth.Handlers;
using PortfolioApp.Application.Use_Cases.Auth.Queries;
using PortfolioApp.Application.Use_Cases.Auth.Validators;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.User;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Enums;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;
using PortfolioApp.Infrastructure.Persistence.Repositories;
using System.Text;

namespace PortfolioApp.AuthAPI;
public static class ServiceRegistrations
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddTransient<JwtTokenHandler>();
        var emailApiUrl = configuration.GetValue<string>("EmailApiUrl");

        if (string.IsNullOrWhiteSpace(emailApiUrl))
        {
            throw new InvalidOperationException("EmailApiUrl is required in appsettings.json");
        }

        services.AddHttpClient("emailApi", c =>
        {
            c.BaseAddress = new Uri(emailApiUrl);
        }).AddHttpMessageHandler<JwtTokenHandler>();

        services.Configure<MVCLinksConfiguration>(configuration.GetSection("MVCLinks"));

        var authDbConnectionString = configuration.GetConnectionString("AuthDb");
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(authDbConnectionString));

        AddJwtAuth(services, configuration);

        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUserVerificationRepository, UserVerificationRepository>();
        services.AddScoped<IRefreshTokenRespository, RefreshTokenRespository>();

        services.AddAutoMapper(typeof(MappingProfile));

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();

		services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.AddScoped<IRequestHandler<RenewPasswordVerifyEmailCommand, ServiceResult<string>>, RenewPasswordVerifyEmailHandler>();
        services.AddScoped<IRequestHandler<ForgotPasswordCommand, ServiceResult>, ForgotPasswordHandler>();
        services.AddScoped<IRequestHandler<LoginCommand, ServiceResult<TokensDto>>, LoginHandler>();
        services.AddScoped<IRequestHandler<NewPasswordCommand, ServiceResult>, NewPasswordHandler>();
        services.AddScoped<IRequestHandler<NewVerificationCommand, ServiceResult>, NewVerificationHandler>();
        services.AddScoped<IRequestHandler<RefreshTokenCommand, ServiceResult<TokensDto>>, RefreshTokenHandler>();
        services.AddScoped<IRequestHandler<RegisterCommand, ServiceResult<RegistrationError>>, RegisterHandler>();
        services.AddScoped<IRequestHandler<RevokeTokenCommand, ServiceResult>, RevokeTokenHandler>();
        services.AddScoped<IRequestHandler<UserProfileQuery, ServiceResult<UserProfileDto>>, UserProfileHandler>();
        services.AddScoped<IRequestHandler<VerifyEmailCommand, ServiceResult>, VerifyEmailHandler>();
        services.AddScoped<IRequestHandler<GetAllUsersQuery, ServiceResult<List<AllUsersDto>>>, GetAllUsersHandler>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistrations).Assembly));

        return services;
    }

    private static void AddJwtAuth(IServiceCollection services, IConfiguration configuration)
    {
        var tokenOptions = configuration.GetSection("Jwt").Get<JwtTokenOptions>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = tokenOptions.Issuer,
                ValidateIssuer = true,
                ValidAudience = tokenOptions.Audience,
                ValidateAudience = true,
                ValidateLifetime = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions.Key)),
                ValidateIssuerSigningKey = true,
                ClockSkew = TimeSpan.Zero
            };

            options.MapInboundClaims = true;

            options.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },

                OnChallenge = async context =>
                {
                    // Token geçersiz olduğu için 401 döndürüyoruz
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    context.Response.ContentType = "application/json";
                    await context.Response.WriteAsync("{\"error\": \"Unauthorized. Please login.\"}");

                    context.HandleResponse(); // Yanıt tamamlandı, isteği durdur
                },

                OnForbidden = context =>
                {
                    // Yetki sorunu varsa 403 Forbidden dön
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    context.Response.ContentType = "application/json";
                    return context.Response.WriteAsync("{\"error\": \"Forbidden. You do not have access to this resource.\"}");
                }
            };
        });
    }
}
