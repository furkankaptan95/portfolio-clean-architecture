using AutoMapper;
using PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
using PortfolioApp.Core.DTOs.Admin.AboutMe;

namespace PortfolioApp.AdminMVC.Mappers;
public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<AboutMeDto, AboutMeViewModel>();
        CreateMap<AddAboutMeViewModel, AddAboutMeMvcDto>();
        CreateMap<AddAboutMeMvcDto, AddAboutMeApiDto>();
    }
}
