using AutoMapper;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Services;
public class HomeService
{
    private readonly IAboutMeService _aboutMeService;
    private readonly IMapper _mapper;
    public HomeService(IMapper mapper, IAboutMeService aboutMeService)
    {
        _aboutMeService = aboutMeService;
        _mapper = mapper;
    }

    public async Task<HomeIndexViewModel> GetHomeModel()
    {
        var model = new HomeIndexViewModel();

        model.AboutMe = await AboutMe();

        model.PersonalInfo = new PersonalInfoViewModel();

        var educations = new List<EducationViewModel>();
        var education = new EducationViewModel();
        educations.Add(education);
        model.Educations = educations;

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
}
