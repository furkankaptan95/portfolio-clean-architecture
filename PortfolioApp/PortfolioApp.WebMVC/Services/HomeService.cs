using AutoMapper;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Services;
public class HomeService
{
    private readonly IAboutMeService _aboutMeService;
    private readonly IEducationService _educationService;
    private readonly IExperienceService _experienceService;
    private readonly IProjectService _projectService;

    private readonly IMapper _mapper;
    public HomeService(IMapper mapper, IAboutMeService aboutMeService, IEducationService educationService,IExperienceService experienceService, IProjectService projecService)
    {
        _aboutMeService = aboutMeService;
        _mapper = mapper;
        _educationService = educationService;
        _experienceService = experienceService;
        _projectService = projecService;
    }

    public async Task<HomeIndexViewModel> GetHomeModel()
    {
        var model = new HomeIndexViewModel();

        model.AboutMe = await AboutMe();

        model.PersonalInfo = new PersonalInfoViewModel();

        model.Educations = await Educations();

        model.Projects = await Projects();

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

    private async Task<List<ProjectViewModel>> Projects()
    {
        var result = await _projectService.GetAllAsync();
        return _mapper.Map<List<ProjectViewModel>>(result.Data);
    }
}
