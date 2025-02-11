using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.AdminMVC.Models.ViewModels.Experience;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
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
            return RedirectToAction("Create");
        }

        ViewData["ErrorMessage"] = result.Message;
        return View(model);
    }

}
