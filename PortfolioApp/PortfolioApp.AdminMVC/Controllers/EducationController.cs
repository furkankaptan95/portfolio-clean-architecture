using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.AdminMVC.Models.ViewModels.Education;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
public class EducationController : Controller
{
    private readonly IEducationService _educationService;
    private readonly IMapper _mapper;
    public EducationController(IMapper mapper, IEducationService educationService)
    {
        _mapper = mapper;
        _educationService = educationService;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddEducationViewModel model)
    {
        var dto = _mapper.Map<AddEducationDto>(model);

        var result = await _educationService.AddAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Create));
        }

        ViewData["ErrorMessage"] = result.Message;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _educationService.GetAllAsync();

        var models = _mapper.Map<List<EducationViewModel>>(result.Data);

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

        var result = await _educationService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        var model = _mapper.Map<UpdateEducationViewModel>(result.Data);

        return View(model);
    }
}
