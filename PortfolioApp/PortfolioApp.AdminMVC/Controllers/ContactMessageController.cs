using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.AdminMVC.Models.ViewModels.ContactMessage;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
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

    [HttpGet]
    public async Task<IActionResult> Reply([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _contactMessageService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        var messageModel = _mapper.Map<ContactMessageViewModel>(result.Data);

        return View(messageModel);
    }

    [HttpPost]
    public async Task<IActionResult> Reply([FromForm] ReplyContactMessageViewModel model)
    {
        if (!ModelState.IsValid)
        {
            TempData["MessageError"] = "Mesaj alanı boş olamaz.";

            return Redirect($"/ContactMessage/Reply/{model.Id}");
        }

        var dto = new ReplyContactMessageDto
        {
            Id = model.Id,
            ReplyMessage = model.Message,
        };

        var result = await _contactMessageService.ReplyAsync(dto);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));

    }

    [HttpGet]
    public async Task<IActionResult> MakeRead([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _contactMessageService.MakeReadAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }
}
