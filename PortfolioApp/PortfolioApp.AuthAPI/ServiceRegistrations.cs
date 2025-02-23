using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
using PortfolioApp.Infrastructure.Persistence.DbContexts;

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

        services.AddScoped<IAuthService, AuthService>();

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

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistrations).Assembly));

        return services;
    }
    
}
