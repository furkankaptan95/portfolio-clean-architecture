using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationController : ControllerBase
{
    private readonly IEducationService _educationService;
    public EducationController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddEducationDto dto)
    {
        var result = await _educationService.AddAsync(dto);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _educationService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _educationService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateEducationDto dto)
    {
        var result = await _educationService.UpdateAsync(dto);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
