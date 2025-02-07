using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AboutMeController : ControllerBase
{
    private readonly IAboutMeService _aboutMeService;

    public AboutMeController(IAboutMeService aboutMeService)
    {
        _aboutMeService = aboutMeService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddAboutMeApiDto dto)
    {
        var result = await _aboutMeService.CreateAboutMeAsync(dto);
        return Ok(result);
    }
}
