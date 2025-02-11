using AutoMapper;
using PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

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
    }
}
