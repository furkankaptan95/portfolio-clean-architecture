using AutoMapper;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Services;
public class HomeService
{
    private readonly IAboutMeService _aboutMeService;
    private readonly IEducationService _educationService;
    private readonly IExperienceService _experienceService;
   private readonly IMapper _mapper;
    public HomeService(IMapper mapper, IAboutMeService aboutMeService, IEducationService educationService,IExperienceService experienceService)
    {
        _aboutMeService = aboutMeService;
        _mapper = mapper;
        _educationService = educationService;
        _experienceService = experienceService;
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

        model.Experiences = await Experiences();

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

    private async Task<List<ExperienceViewModel>> Experiences()
    {
        var result = await _experienceService.GetAllAsync();
        return _mapper.Map<List<ExperienceViewModel>>(result.Data);
    }
}
