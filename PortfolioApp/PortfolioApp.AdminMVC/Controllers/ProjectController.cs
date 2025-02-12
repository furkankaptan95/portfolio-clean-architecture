using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Project;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
public class ProjectController : Controller
{
    private readonly IProjectService _projecService;
    private readonly IMapper _mapper;
    public ProjectController(IMapper mapper, IProjectService projecService)
    {
        _projecService = projecService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddProjectViewModel model)
    {
        var dto = _mapper.Map<AddMvcProjectDto>(model);

        var result = await _projecService.AddAsync(dto);

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
        var result = await _projecService.GetAllAsync();

        var models = _mapper.Map<List<ProjectViewModel>>(result.Data);

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

        var result = await _projecService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        var model = _mapper.Map<UpdateProjectViewModel>(result.Data);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateProjectViewModel model)
    {
        var dto = _mapper.Map<UpdateProjectMVCDto>(model);

        var result = await _projecService.UpdateAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        TempData["ErrorMessage"] = result.Message;
        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> Visibility([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz Blog Post ID'si.";
            return RedirectToAction(nameof(All));
        }

        var result = await _projecService.ChangeVisibilityAsync(id);

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
            TempData["ErrorMessage"] = "Geçersiz Blog Post ID'si.";
            return RedirectToAction(nameof(All));
        }

        var result = await _projecService.DeleteAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }
}
