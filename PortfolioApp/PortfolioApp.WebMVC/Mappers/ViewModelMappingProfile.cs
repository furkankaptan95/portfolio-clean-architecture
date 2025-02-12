using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.WebMVC.Models.ViewModels;
using AutoMapper;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.DTOs.Admin.Experience;

namespace PortfolioApp.WebMVC.Mappers;
public class ViewModelMappingProfile : Profile
{
    public ViewModelMappingProfile()
    {
        CreateMap<AboutMeDto, AboutMeViewModel>();
        CreateMap<EducationDto, EducationViewModel>();
        CreateMap<ExperienceDto, ExperienceViewModel>();


    }
    
}
