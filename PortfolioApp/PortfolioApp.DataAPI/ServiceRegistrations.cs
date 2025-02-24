using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Application.Mappers;
using PortfolioApp.Application.Use_Cases.AboutMe.Commands;
using PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
using PortfolioApp.Application.Use_Cases.AboutMe.Queries;
using PortfolioApp.Application.Use_Cases.AboutMe.Validators;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Application.Use_Cases.BlogPost.Handlers;
using PortfolioApp.Application.Use_Cases.BlogPost.Queries;
using PortfolioApp.Application.Use_Cases.Comment.Commands;
using PortfolioApp.Application.Use_Cases.Comment.Handlers;
using PortfolioApp.Application.Use_Cases.Comment.Queries;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
using PortfolioApp.Application.Use_Cases.ContactMessage.Queries;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Application.Use_Cases.Education.Handlers;
using PortfolioApp.Application.Use_Cases.Education.Queries;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Application.Use_Cases.Experience.Handlers;
using PortfolioApp.Application.Use_Cases.Experience.Queries;
using PortfolioApp.Application.Use_Cases.Home.Handler;
using PortfolioApp.Application.Use_Cases.Home.Queries;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Handlers;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Queries;
using PortfolioApp.Application.Use_Cases.Project.Commands;
using PortfolioApp.Application.Use_Cases.Project.Handlers;
using PortfolioApp.Application.Use_Cases.Project.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.DTOs.Admin.Home;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.DTOs.Web.BlogPost;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;
using PortfolioApp.Infrastructure.Persistence.Repositories;
using System.Text;

namespace PortfolioApp.DataAPI;
public static class ServiceRegistrations
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<JwtTokenHandler>();

        var dataDbConnectionString = configuration.GetConnectionString("DataDb");
        services.AddDbContext<DataDbContext>(options =>
            options.UseSqlServer(dataDbConnectionString));

		AddJwtAuth(services, configuration);

        services.AddScoped<HomeService>();
        services.AddScoped<IHomeRepository, HomeRepository>();

        services.AddScoped<IAboutMeService, AboutMeService>();
        services.AddScoped<IAboutMeRepository, AboutMeRepository>();

        services.AddScoped<IBlogPostService, BlogPostService>();
		services.AddScoped<IBlogPostRepository, BlogPostRepository>();

        services.AddScoped<ICommentService, CommentService>();
        services.AddScoped<ICommentRepository, CommentRepository>();

        services.AddScoped<IContactMessageService, ContactMessageService>();
        services.AddScoped<IContactMessageRepository, ContactMessageRepository>();

        services.AddScoped<IEducationService, EducationService>();
        services.AddScoped<IEducationRepository, EducationRepository>();

        services.AddScoped<IExperienceService, ExperienceService>();
        services.AddScoped<IExperienceRepository, ExperienceRepository>();

        services.AddScoped<IPersonalInfoService, PersonalInfoService>();
        services.AddScoped<IPersonalInfoRepository, PersonalInfoRepository>();

        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IProjectRepository, ProjectRepository>();

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
		}).AddHttpMessageHandler<JwtTokenHandler>();

		services.AddAboutMeHandlers();
		services.AddBlogPostHandlers();
		services.AddCommentHandlers();
		services.AddContactMessageHandlers();
        services.AddEducationHandlers();
        services.AddExperienceHandlers();
        services.AddProjectHandlers();
        services.AddPersonalInfoHandlers();
        services.AddScoped<IRequestHandler<GetHomeInfosQuery, ServiceResult<HomeDto>>, GetHomeInfosHandler>();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ServiceRegistrations).Assembly));
        return services;
    }

	private static IServiceCollection AddAboutMeHandlers(this IServiceCollection services)
	{
        services.AddScoped<IRequestHandler<CreateAboutMeCommand, ServiceResult>, CreateAboutMeHandler>();
        services.AddScoped<IRequestHandler<GetAboutMeQuery, ServiceResult<AboutMeDto>>, GetAboutMeHandler>();
        services.AddScoped<IRequestHandler<UpdateAboutMeCommand, ServiceResult>, UpdateAboutMeHandler>();

        return services;
	}

    private static IServiceCollection AddPersonalInfoHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreatePersonalInfoCommand, ServiceResult>, CreatePersonalInfoHandler>();
        services.AddScoped<IRequestHandler<GetPersonalInfoQuery, ServiceResult<PersonalInfoDto>>, GetPersonalInfoHandler>();
        services.AddScoped<IRequestHandler<UpdatePersonalInfoCommand, ServiceResult>, UpdatePersonalInfoHandler>();

        return services;
    }

    private static IServiceCollection AddBlogPostHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<BlogPostVisibilityCommand, ServiceResult>, BlogPostVisibilityHandler>();
        services.AddScoped<IRequestHandler<GetBlogPostByIdQuery, ServiceResult<BlogPostDto>>, GetBlogPostByIdHandler>();
        services.AddScoped<IRequestHandler<CreateBlogPostCommand, ServiceResult>, CreateBlogPostHandler>();
        services.AddScoped<IRequestHandler<DeleteBlogPostCommand, ServiceResult>, DeleteBlogPostHandler>();
        services.AddScoped<IRequestHandler<GetBlogPostByIdWebQuery, ServiceResult<BlogPostWebDto>>, GetBlogPostByIdWebHandler>();
        services.AddScoped<IRequestHandler<GetBlogPostsQuery, ServiceResult<List<BlogPostDto>>>, GetBlogPostsHandler>();
        services.AddScoped<IRequestHandler<UpdateBlogPostCommand, ServiceResult>, UpdateBlogPostHandler>();

        return services;
    }

    private static IServiceCollection AddCommentHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CommentApprovalCommand, ServiceResult>, CommentApprovalHandler>();
        services.AddScoped<IRequestHandler<CreateCommentCommand, ServiceResult>, CreateCommentHandler>();
        services.AddScoped<IRequestHandler<DeleteCommentCommand, ServiceResult>, DeleteCommentHandler>();
        services.AddScoped<IRequestHandler<GetCommentsQuery, ServiceResult<List<CommentDto>>>, GetCommentsHandler>();

        return services;
    }

    private static IServiceCollection AddContactMessageHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateContactMessageCommand, ServiceResult>, CreateContactMessageHandler>();
        services.AddScoped<IRequestHandler<GetContactMessageByIdQuery, ServiceResult<ContactMessageDto>>, GetContactMessageByIdHandler>();
        services.AddScoped<IRequestHandler<MakeContactMessageReadCommand, ServiceResult>, MakeContactMessageReadHandler>();
        services.AddScoped<IRequestHandler<GetContactMessagesQuery, ServiceResult<List<ContactMessageDto>>>, GetContactMessagesHandler>();
        services.AddScoped<IRequestHandler<ReplyContactMessageCommand, ServiceResult>, ReplyContactMessageHandler>();

        return services;
    }

    private static IServiceCollection AddEducationHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateEducationCommand, ServiceResult>, CreateEducationHandler>();
        services.AddScoped<IRequestHandler<GetEducationByIdQuery, ServiceResult<EducationDto>>, GetEducationByIdHandler>();
        services.AddScoped<IRequestHandler<DeleteEducationCommand, ServiceResult>, DeleteEducationHandler>();
        services.AddScoped<IRequestHandler<GetEducationsQuery, ServiceResult<List<EducationDto>>>, GetEducationsHandler>();
        services.AddScoped<IRequestHandler<EducationVisibilityCommand, ServiceResult>, EducationVisibilityHandler>();
        services.AddScoped<IRequestHandler<UpdateEducationCommand, ServiceResult>, UpdateEducationHandler>();

        return services;
    }

    private static IServiceCollection AddExperienceHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateExperienceCommand, ServiceResult>, CreateExperienceHandler>();
        services.AddScoped<IRequestHandler<GetExperienceByIdQuery, ServiceResult<ExperienceDto>>, GetExperienceByIdHandler>();
        services.AddScoped<IRequestHandler<DeleteExperienceCommand, ServiceResult>, DeleteExperienceHandler>();
        services.AddScoped<IRequestHandler<GetExperiencesQuery, ServiceResult<List<ExperienceDto>>>, GetExperiencesHandler>();
        services.AddScoped<IRequestHandler<ExperienceVisibilityCommand, ServiceResult>, ExperienceVisibilityHandler>();
        services.AddScoped<IRequestHandler<UpdateExperienceCommand, ServiceResult>, UpdateExperienceHandler>();

        return services;
    }

    private static IServiceCollection AddProjectHandlers(this IServiceCollection services)
    {
        services.AddScoped<IRequestHandler<CreateProjectCommand, ServiceResult>, CreateProjectHandler>();
        services.AddScoped<IRequestHandler<GetProjectByIdQuery, ServiceResult<ProjectDto>>, GetProjectByIdHandler>();
        services.AddScoped<IRequestHandler<DeleteProjectCommand, ServiceResult>, DeleteProjectHandler>();
        services.AddScoped<IRequestHandler<GetProjectsQuery, ServiceResult<List<ProjectDto>>>, GetProjectsHandler>();
        services.AddScoped<IRequestHandler<ProjectVisibilityCommand, ServiceResult>, ProjectVisibilityHandler>();
        services.AddScoped<IRequestHandler<UpdateProjectCommand, ServiceResult>, UpdateProjectHandler>();

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
