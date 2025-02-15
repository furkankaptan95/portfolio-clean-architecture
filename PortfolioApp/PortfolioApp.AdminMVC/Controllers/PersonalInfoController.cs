using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.PersonalInfo;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;

[Authorize(Roles = "Admin")]
public class PersonalInfoController : Controller
{
    private readonly IPersonalInfoService _personalInfoService;
    private readonly IMapper _mapper;
    public PersonalInfoController(IMapper mapper, IPersonalInfoService personalInfoService)
    {
        _personalInfoService = personalInfoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var result = await _personalInfoService.GetAsync();

        if (result.IsSuccess)
            return RedirectToAction("PersonalInfo");

        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddPersonalInfoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<AddPersonalInfoDto>(model);

        var result = await _personalInfoService.AddAsync(dto);

        if (result.IsSuccess)
            TempData["Message"] = result.Message;
        else
            TempData["ErrorMessage"] = result.Message;

        return RedirectToAction("PersonalInfo");
    }

    [HttpGet]
    public async Task<IActionResult> PersonalInfo()
    {
        var result = await _personalInfoService.GetAsync();

        if (!result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Create));
        }

        var model = _mapper.Map<PersonalInfoViewModel>(result.Data);

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> Update()
    {
        var result = await _personalInfoService.GetAsync();

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(Create));
        }

        var model = _mapper.Map<UpdatePersonalInfoViewModel>(result.Data);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdatePersonalInfoViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<UpdatePersonalInfoDto>(model);

        var result = await _personalInfoService.UpdateAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(PersonalInfo));
        }

        TempData["ErrorMessage"] = result.Message;
        return Redirect("/");
    }
}
