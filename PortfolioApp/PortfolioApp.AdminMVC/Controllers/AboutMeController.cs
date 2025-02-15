using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
public class AboutMeController : Controller
{
    private readonly IAboutMeService _aboutMeService;
    private readonly IMapper _mapper;
    public AboutMeController(IMapper mapper, IAboutMeService aboutMeService)
    {
        _aboutMeService = aboutMeService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> AboutMe()
    {
        var result = await _aboutMeService.GetAsync();

        if (!result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Create));
        }

        var model = _mapper.Map<AboutMeViewModel>(result.Data);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var result = await _aboutMeService.GetAsync();

        if (result.IsSuccess)
            return RedirectToAction(nameof(AboutMe));

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddAboutMeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<AddAboutMeMvcDto>(model);

        var result = await _aboutMeService.CreateAboutMeAsync(dto);

        if (result.IsSuccess)
            TempData["Message"] = result.Message;
        else
            TempData["ErrorMessage"] = result.Message;


        return RedirectToAction(nameof(AboutMe));
    }

    [HttpGet]
    public async Task<IActionResult> Update()
    {
        var result = await _aboutMeService.GetAsync();

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(Create));
        }

        var model = _mapper.Map<UpdateAboutMeViewModel>(result.Data);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateAboutMeViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<UpdateAboutMeMVCDto>(model);

        var result = await _aboutMeService.UpdateAboutMeAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(AboutMe));
        }

        TempData["ErrorMessage"] = result.Message;
        return Redirect("/");
    }

    [HttpGet]
    public async Task<IActionResult> DownloadCv([FromQuery] string cvUrl)
    {
        var result = await _aboutMeService.DownloadCvAsync(cvUrl); 

        var fileBytes = result.Data;
        var downloadedFileName = "FurkanKaptanÖzgeçmiş.pdf";

        return File(fileBytes, "application/pdf", downloadedFileName);
    }
}
