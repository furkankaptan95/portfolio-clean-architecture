using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Mappers;
using PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
using PortfolioApp.Application.Use_Cases.AboutMe.Validators;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.Infrastructure.Persistence.DbContexts;
using System.Text;

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

		AddJwtAuth(services, configuration);

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

		var emailApiUrl = configuration.GetValue<string>("EmailApiUrl");

		if (string.IsNullOrWhiteSpace(emailApiUrl))
		{
			throw new InvalidOperationException("EmailApiUrl is required in appsettings.json");
		}

		services.AddHttpClient("emailApi", c =>
		{
			c.BaseAddress = new Uri(emailApiUrl);
		});

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
