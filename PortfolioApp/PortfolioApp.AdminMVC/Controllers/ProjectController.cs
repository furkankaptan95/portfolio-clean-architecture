using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Experience;
using PortfolioApp.AdminMVC.Models.ViewModels.Project;
using PortfolioApp.Core.DTOs.Admin.Experience;
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
            return RedirectToAction(nameof(Create));
        }

        ViewData["ErrorMessage"] = result.Message;
        return View(model);
    }
}
