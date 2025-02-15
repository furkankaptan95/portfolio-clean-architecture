using PortfolioApp.Core.Config;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Mappers;
using PortfolioApp.WebMVC.Services;

namespace PortfolioApp.WebMVC;
public static class ServiceRegistrations
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddControllersWithViews();

        services.Configure<FileApiSettings>(configuration.GetSection("FileApiSettings"));

        var dataApiUrl = configuration.GetValue<string>("DataApiUrl");
        if (string.IsNullOrWhiteSpace(dataApiUrl))
        {
            throw new InvalidOperationException("DataApiUrl is required in appsettings.json");
        }
        services.AddHttpClient("dataApi", c =>
        {
            c.BaseAddress = new Uri(dataApiUrl);
        });

        var fileApiUrl = configuration.GetValue<string>("FileApiUrl");
        if (string.IsNullOrWhiteSpace(fileApiUrl))
        {
            throw new InvalidOperationException("FileApiUrl is required in appsettings.json");
        }
        services.AddHttpClient("fileApi", c =>
        {
            c.BaseAddress = new Uri(fileApiUrl);
        });

        services.AddScoped<IAboutMeService, AboutMeService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IContactMessageService, ContactMessageService>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<ICommentService, CommentService>();

        services.AddScoped<HomeService>();

        services.AddAutoMapper(typeof(ViewModelMappingProfile));

        return services;
    }
}
