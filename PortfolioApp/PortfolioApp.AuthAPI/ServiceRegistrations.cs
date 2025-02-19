using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Mappers;
using PortfolioApp.Application.Use_Cases.AboutMe.Validators;
using PortfolioApp.Application.Use_Cases.Auth.Handlers;
using PortfolioApp.Application.Use_Cases.Auth.Validators;
using PortfolioApp.Core.Common;
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

        var dataDbConnectionString = configuration.GetConnectionString("DataDb");
        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(dataDbConnectionString));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly));
        services.AddScoped<IAuthService, AuthService>();

		services.AddAutoMapper(typeof(MappingProfile));

		services.AddFluentValidationAutoValidation();
		services.AddValidatorsFromAssemblyContaining<LoginDtoValidator>();

		services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
