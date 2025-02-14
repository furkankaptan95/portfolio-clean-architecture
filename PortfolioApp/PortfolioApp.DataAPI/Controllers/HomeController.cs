using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Application.Business_Logic.Services;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HomeController(HomeService homeService) : ControllerBase
{
    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        var result = await homeService.GetHomeInfosAsync();

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
