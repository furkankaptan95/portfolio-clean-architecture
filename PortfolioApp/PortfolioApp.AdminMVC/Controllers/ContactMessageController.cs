using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.ContactMessage;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
public class ContactMessageController : Controller
{
    private readonly IContactMessageService _contactMessageService;
    private readonly IMapper _mapper;
    public ContactMessageController(IContactMessageService contactMessageService, IMapper mapper)
    {
        _contactMessageService = contactMessageService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _contactMessageService.GetAllAsync();

        var model = _mapper.Map<List<ContactMessageViewModel>>(result.Data);

        return View(model);
    }
}
