using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Common.Configurations;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.FileAPI;
public static class ServiceRegistrations
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
        });

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        services.Configure<FileSettings>(configuration.GetSection("FileSettings"));
        services.AddScoped<IFileService, FileService>();

        return services;
    }
}