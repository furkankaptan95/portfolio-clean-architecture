using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Project;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;

[Authorize(Roles = "Admin")]
public class ProjectController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;
    public ProjectController(IMapper mapper, IProjectService projectService)
    {
        _projectService = projectService;
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
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<AddMvcProjectDto>(model);

        var result = await _projectService.AddAsync(dto);

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
        var result = await _projectService.GetAllAsync();

        var models = _mapper.Map<List<ProjectViewModel>>(result.Data);

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

        var result = await _projectService.GetByIdAsync(id);

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
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<UpdateProjectMVCDto>(model);

        var result = await _projectService.UpdateAsync(dto);

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
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _projectService.ChangeVisibilityAsync(id);

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

        var result = await _projectService.DeleteAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }
}
