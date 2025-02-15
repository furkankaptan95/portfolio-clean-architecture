using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Mappers;
using PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
using PortfolioApp.Application.Use_Cases.AboutMe.Validators;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.DataAPI;
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

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAboutMeHandler).Assembly));
        services.AddScoped<HomeService>();
        services.AddScoped<IAboutMeService, AboutMeService>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IContactMessageService, ContactMessageService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IProjectService, ProjectService>();

        services.AddAutoMapper(typeof(MappingProfile));

        services.AddFluentValidationAutoValidation();
        services.AddValidatorsFromAssemblyContaining<AddAboutMeApiDtoValidator>();

        services.AddControllers();

        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }
}
