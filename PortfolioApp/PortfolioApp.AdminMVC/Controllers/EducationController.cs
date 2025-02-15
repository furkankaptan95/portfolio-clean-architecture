using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Education;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;

[Authorize(Roles = "Admin")]
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
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<AddEducationDto>(model);

        var result = await _educationService.AddAsync(dto);

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
        var result = await _educationService.GetAllAsync();

        var models = _mapper.Map<List<EducationViewModel>>(result.Data);

        return View(models);
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
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

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateEducationViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<UpdateEducationDto>(model);

        var result = await _educationService.UpdateAsync(dto);

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
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _educationService.ChangeVisibilityAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _educationService.DeleteAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }
}
