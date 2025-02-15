using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Core.DTOs.Web.ContactMessage;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ContactMessageController : ControllerBase
{
    private readonly IContactMessageService _contactMessageService;
    public ContactMessageController(IContactMessageService contactMessageService)
    {
        _contactMessageService = contactMessageService;
    }
    [AllowAnonymous]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddContactMessageDto dto)
    {
        var result = await _contactMessageService.AddAsync(dto);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _contactMessageService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _contactMessageService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("reply")]
    public async Task<IActionResult> Reply([FromBody] ReplyContactMessageDto dto)
    {
        var result = await _contactMessageService.ReplyAsync(dto);

        return Ok(result);
    }

    [HttpGet("make-read/{id:int}")]
    public async Task<IActionResult> MakeRead([FromRoute] int id)
    {
        var result = await _contactMessageService.MakeReadAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
