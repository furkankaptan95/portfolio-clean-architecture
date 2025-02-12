using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Web.ContactMessage;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Controllers;
public class ContactMessageController : Controller
{
    private readonly IContactMessageService _contactMessageService;
    private readonly IMapper _mapper;
    public ContactMessageController(IContactMessageService contactMessageService, IMapper mapper)
    {
        _contactMessageService = contactMessageService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddContactMessageViewModel model)
    {
        var dto = _mapper.Map<AddContactMessageDto>(model);
        var result = await _contactMessageService.AddAsync(dto);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return Redirect("/#contact");
        }

        TempData["Message"] = result.Message;
        return Redirect("/#contact");
    }
}
