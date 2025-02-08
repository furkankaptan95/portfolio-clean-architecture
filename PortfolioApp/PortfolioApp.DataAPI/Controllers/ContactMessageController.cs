using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Web.ContactMessage;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContactMessageController : ControllerBase
{
    private readonly IContactMessageService _contactMessageService;
    public ContactMessageController(IContactMessageService contactMessageService)
    {
        _contactMessageService = contactMessageService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddContactMessageDto dto)
    {
        var result = await _contactMessageService.AddAsync(dto);

        return Ok(result);
    }
}
