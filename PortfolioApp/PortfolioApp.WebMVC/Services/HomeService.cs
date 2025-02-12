using AutoMapper;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Services;
public class HomeService
{
    private readonly IAboutMeService _aboutMeService;
    private readonly IEducationService _educationService;
    private readonly IMapper _mapper;
    public HomeService(IMapper mapper, IAboutMeService aboutMeService, IEducationService educationService)
    {
        _aboutMeService = aboutMeService;
        _mapper = mapper;
        _educationService = educationService;
    }

    public async Task<HomeIndexViewModel> GetHomeModel()
    {
        var model = new HomeIndexViewModel();

        model.AboutMe = await AboutMe();

        model.PersonalInfo = new PersonalInfoViewModel();

        model.Educations = await Educations();

        var projects = new List<ProjectViewModel>();
        var project = new ProjectViewModel();
        projects.Add(project);
        model.Projects = projects;

        var experiences = new List<ExperienceViewModel>();
        var experience = new ExperienceViewModel();
        experiences.Add(experience);
        model.Experiences = experiences;

        return model;
    }

    private async Task<AboutMeViewModel> AboutMe()
    {
       var result = await _aboutMeService.GetAsync();
       return _mapper.Map<AboutMeViewModel>(result.Data);
    }

    private async Task<List<EducationViewModel>> Educations()
    {
        var result = await _educationService.GetAllAsync();
        return _mapper.Map<List<EducationViewModel>>(result.Data);
    }
}
