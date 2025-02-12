using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.WebMVC.Models.ViewModels;
using AutoMapper;
using PortfolioApp.Core.DTOs.Admin.Education;

namespace PortfolioApp.WebMVC.Mappers;
public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<AboutMeDto, AboutMeViewModel>();
        CreateMap<EducationDto, EducationViewModel>();
    }
    
}
