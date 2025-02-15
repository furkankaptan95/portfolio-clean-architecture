using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using PortfolioApp.AdminMVC.Mappers;
using PortfolioApp.AdminMVC.Service;
using PortfolioApp.AdminMVC.Services;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Config;
using PortfolioApp.Core.Interfaces;
using System.Text;

namespace PortfolioApp.AdminMVC;
public static class ServiceRegistrations
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
		services.AddTransient<JwtAndRefreshTokenHandler>();
		AddJwtAuth(services, configuration);
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
        }).AddHttpMessageHandler<JwtAndRefreshTokenHandler>();



		var fileApiUrl = configuration.GetValue<string>("FileApiUrl");
        if (string.IsNullOrWhiteSpace(fileApiUrl))
        {
            throw new InvalidOperationException("FileApiUrl is required in appsettings.json");
        }
        services.AddHttpClient("fileApi", c =>
        {
            c.BaseAddress = new Uri(fileApiUrl);
        }).AddHttpMessageHandler<JwtAndRefreshTokenHandler>();



		var authApiUrl = configuration.GetValue<string>("AuthApiUrl");

        if (string.IsNullOrWhiteSpace(authApiUrl))
        {
            throw new InvalidOperationException("AuthApiUrl is required in appsettings.json");
        }

        services.AddHttpClient("authApi", c =>
        {
            c.BaseAddress = new Uri(authApiUrl);
        }).AddHttpMessageHandler<JwtAndRefreshTokenHandler>();




		services.AddScoped<HomeService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAboutMeService, AboutMeService>();
        services.AddScoped<IBlogPostService, BlogPostService>();
        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<IContactMessageService, ContactMessageService>();

        services.AddAutoMapper(typeof(ViewModelMappingProfile));


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
                    var accessToken = context.Request.Cookies["JwtToken"];

                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        context.Token = accessToken;
                    }

                    return Task.CompletedTask;
                },


                OnChallenge = async context =>
                {
                    var refreshToken = context.Request.Cookies["RefreshToken"];

                    if (!string.IsNullOrEmpty(refreshToken))
                    {
                        var authService = context.HttpContext.RequestServices.GetRequiredService<IAuthService>();
                        var result = await authService.RefreshTokenAsync(refreshToken);

                        if (result.IsSuccess)
                        {
                            context.Response.Cookies.Append("JwtToken", result.Data.JwtToken, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = DateTime.UtcNow.AddMinutes(10)
                            });

                            context.Response.Cookies.Append("RefreshToken", result.Data.RefreshToken, new CookieOptions
                            {
                                HttpOnly = true,
                                Secure = true,
                                SameSite = SameSiteMode.Strict,
                                Expires = DateTime.UtcNow.AddDays(7)
                            });

                            context.HandleResponse();
                            context.HttpContext.Response.Redirect(context.Request.Path.Value);
                            return;
                        }
                    }

                    context.Response.Redirect("/Auth/Login");
                    context.HandleResponse();
                },

                OnForbidden = context =>
                {
                    // Yetki sorunu varsa, MVC'ye yönlendirme yap
                    context.Response.Redirect("/Auth/Forbidden"); // MVC'de Forbidden sayfasına yönlendir
                    return Task.CompletedTask; // Yönlendirme sonrası işlemi sonlandır
                }
            };
        });
    }
}
