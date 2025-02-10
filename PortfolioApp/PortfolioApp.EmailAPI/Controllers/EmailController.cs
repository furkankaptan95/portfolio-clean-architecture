using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Email;

namespace PortfolioApp.EmailAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequestDto emailRequest)
    {
        return Ok();
    }
}
