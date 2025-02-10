using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Email;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.EmailAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    
    [HttpPost("send")]
    public async Task<IActionResult> SendEmailAsync([FromBody] EmailRequestDto emailRequest)
    {
        var result = await _emailService.SendEmailAsync(emailRequest);

        if (result)
            return Ok("Email başarıyla gönderildi.");

        return StatusCode(500,"Email gönderilirken bir hata oluştu!..");
    }
}
