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
    IPersonalInfoService _personalInfoService;

    private readonly IMapper _mapper;
    public HomeService(IMapper mapper, IAboutMeService aboutMeService, IEducationService educationService,IExperienceService experienceService, IProjectService projectService, IPersonalInfoService personalInfoService)
    {
        _aboutMeService = aboutMeService;
        _mapper = mapper;
        _educationService = educationService;
        _experienceService = experienceService;
        _projectService = projectService;
        _personalInfoService = personalInfoService;
    }

    public async Task<HomeIndexViewModel> GetHomeModel()
    {
        var model = new HomeIndexViewModel();

        model.AboutMe = await AboutMe();

        model.PersonalInfo = await PersonalInfo();

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
    private async Task<PersonalInfoViewModel> PersonalInfo()
    {
        var result = await _personalInfoService.GetAsync();
        return _mapper.Map<PersonalInfoViewModel>(result.Data);
    }

    private async Task<List<EducationViewModel>> Educations()
    {
        var result = await _educationService.GetAllAsync();
        var visibleDtos = result.Data.Where(e=>e.IsVisible == true).ToList();
        return _mapper.Map<List<EducationViewModel>>(visibleDtos);
    }

    private async Task<List<ExperienceViewModel>> Experiences()
    {
        var result = await _experienceService.GetAllAsync();
        var visibleDtos = result.Data.Where(e => e.IsVisible == true).ToList();
        return _mapper.Map<List<ExperienceViewModel>>(visibleDtos);
    }

    private async Task<List<ProjectViewModel>> Projects()
    {
        var result = await _projectService.GetAllAsync();
        var visibleDtos = result.Data.Where(e => e.IsVisible == true).ToList();
        return _mapper.Map<List<ProjectViewModel>>(visibleDtos);
    }
}
