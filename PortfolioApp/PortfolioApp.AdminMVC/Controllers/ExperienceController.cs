using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.AdminMVC.Models.ViewModels.Education;
using PortfolioApp.AdminMVC.Models.ViewModels.Experience;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
public class ExperienceController : Controller
{
    private readonly IExperienceService _experienceService;
    private readonly IMapper _mapper;
    public ExperienceController(IMapper mapper, IExperienceService experienceService)
    {
        _experienceService = experienceService;
        _mapper = mapper;
    }
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddExperienceViewModel model)
    {
        var dto = _mapper.Map<AddExperienceDto>(model);

        var result = await _experienceService.AddAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        ViewData["ErrorMessage"] = result.Message;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _experienceService.GetAllAsync();

        var models = _mapper.Map<List<ExperienceViewModel>>(result.Data);

        return View(models);
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz Blog Post ID'si.";
            return RedirectToAction(nameof(All));
        }

        var result = await _experienceService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        var model = _mapper.Map<UpdateExperienceViewModel>(result.Data);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateExperienceViewModel model)
    {
        var dto = _mapper.Map<UpdateExperienceDto>(model);

        var result = await _experienceService.UpdateAsync(dto);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Visibility([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz Blog Post ID'si.";
            return RedirectToAction(nameof(All));
        }

        var result = await _experienceService.ChangeVisibilityAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }
}
