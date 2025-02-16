using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.WebMVC.Models.ViewModels;
using AutoMapper;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.DTOs.Web.ContactMessage;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.WebMVC.Mappers;
public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<AboutMeDto, AboutMeViewModel>();
        CreateMap<EducationDto, EducationViewModel>();
        CreateMap<ExperienceDto, ExperienceViewModel>();
        CreateMap<ProjectDto, ProjectViewModel>();
        CreateMap<PersonalInfoDto, PersonalInfoViewModel>();
        CreateMap<AddContactMessageViewModel, AddContactMessageDto>();
        CreateMap<CommentWebDto, CommentViewModel>();
        CreateMap<BlogPostDto, AllBlogPostsViewModel>();
        CreateMap<AddCommentViewModel, AddCommentDto>();
		CreateMap<LoginViewModel, LoginDto>();
        CreateMap<RegisterViewModel, RegisterDto>();
		CreateMap<ForgotPasswordViewModel, ForgotPasswordDto>();
		CreateMap<NewPasswordViewModel, NewPasswordDto>();
        CreateMap<NewVerificationViewModel, NewVerificationMailDto>();
    }

}
