﻿using AutoMapper;
using PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.AdminMVC.Models.ViewModels.Education;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Education;

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
    }
}
