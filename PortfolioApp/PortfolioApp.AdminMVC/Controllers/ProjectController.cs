using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
}
