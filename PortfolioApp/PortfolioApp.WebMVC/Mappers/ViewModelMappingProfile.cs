using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.WebMVC.Models.ViewModels;
using AutoMapper;

namespace PortfolioApp.WebMVC.Mappers;
public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<AboutMeDto, AboutMeViewModel>();
    }
    
}
