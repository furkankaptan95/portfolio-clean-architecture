using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{
    private readonly IProjectService _projectService;
    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddProjectDto dto)
    {
        var result = await _projectService.AddAsync(dto);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _projectService.GetAllAsync();

        return Ok(result);
    }
}
