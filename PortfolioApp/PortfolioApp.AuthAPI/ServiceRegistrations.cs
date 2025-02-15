using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Use_Cases.Auth.Handlers;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.AuthAPI;
public static class ServiceRegistrations
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        var authDbConnectionString = configuration.GetConnectionString("AuthDb");
        services.AddDbContext<AuthDbContext>(options =>
            options.UseSqlServer(authDbConnectionString));

        var dataDbConnectionString = configuration.GetConnectionString("DataDb");
        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(dataDbConnectionString));

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterHandler).Assembly));
        services.AddScoped<IAuthService, AuthService>();

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
