﻿using AutoMapper;
using PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
using PortfolioApp.AdminMVC.Models.ViewModels.Auth;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.AdminMVC.Models.ViewModels.Comment;
using PortfolioApp.AdminMVC.Models.ViewModels.ContactMessage;
using PortfolioApp.AdminMVC.Models.ViewModels.Education;
using PortfolioApp.AdminMVC.Models.ViewModels.Experience;
using PortfolioApp.AdminMVC.Models.ViewModels.Home;
using PortfolioApp.AdminMVC.Models.ViewModels.PersonalInfo;
using PortfolioApp.AdminMVC.Models.ViewModels.Project;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.DTOs.Admin.Home;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.DTOs.Admin.User;
using PortfolioApp.Core.DTOs.Auth;

namespace PortfolioApp.AdminMVC.Mappers;
public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<AboutMeDto, AboutMeViewModel>();
        CreateMap<AddAboutMeViewModel, AddAboutMeMvcDto>();
        CreateMap<AddAboutMeMvcDto, AddAboutMeApiDto>();
        CreateMap<AboutMeDto, UpdateAboutMeViewModel>();
        CreateMap<UpdateAboutMeViewModel, UpdateAboutMeMVCDto>();
        CreateMap<UpdateAboutMeMVCDto, UpdateAboutMeApiDto>();

        CreateMap<AddBlogPostViewModel, AddBlogPostDto>();
        CreateMap<BlogPostDto, BlogPostViewModel>();
        CreateMap<BlogPostDto, UpdateBlogPostViewModel>();
        CreateMap<UpdateBlogPostViewModel, UpdateBlogPostDto>();

        CreateMap<AddEducationViewModel, AddEducationDto>();
        CreateMap<EducationDto, EducationViewModel>();
        CreateMap<EducationDto, UpdateEducationViewModel>();
        CreateMap<UpdateEducationViewModel,UpdateEducationDto >();

        CreateMap<AddExperienceViewModel, AddExperienceDto>();
        CreateMap<ExperienceDto, ExperienceViewModel>();
        CreateMap<ExperienceDto, UpdateExperienceViewModel>();
        CreateMap<UpdateExperienceViewModel, UpdateExperienceDto>();

        CreateMap<AddPersonalInfoViewModel, AddPersonalInfoDto>();
        CreateMap<PersonalInfoDto, PersonalInfoViewModel>();
        CreateMap<PersonalInfoDto, UpdatePersonalInfoViewModel>();
        CreateMap<UpdatePersonalInfoViewModel, UpdatePersonalInfoDto>();

        CreateMap<AddProjectViewModel, AddMvcProjectDto>();
        CreateMap<AddMvcProjectDto, AddApiProjectDto>();
        CreateMap<ProjectDto, ProjectViewModel>();
        CreateMap<ProjectDto, UpdateProjectViewModel>();
        CreateMap<UpdateProjectViewModel, UpdateProjectMVCDto>();
        CreateMap<UpdateProjectMVCDto, UpdateProjectApiDto>();

        CreateMap<CommentDto, CommentViewModel>();

        CreateMap<ContactMessageDto,ContactMessageViewModel>();

        CreateMap<HomeDto, HomeViewModel>();

		CreateMap<LoginViewModel, LoginDto>(); 
        CreateMap<ForgotPasswordViewModel, ForgotPasswordDto>();
        CreateMap<NewPasswordViewModel, NewPasswordDto>();
        CreateMap<UserProfileDto, UserProfileViewModel>();
        CreateMap<AllUsersDto, AllUsersViewModel>();
    }
}
