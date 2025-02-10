﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
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
        return View();
    }
}
