using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.AboutMe;
using PortfolioApp.AdminMVC.Models.ViewModels.PersonalInfo;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
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
        var dto = _mapper.Map<AddPersonalInfoDto>(model);

        var result = await _personalInfoService.AddAsync(dto);

        if (result.IsSuccess)
            TempData["Message"] = result.Message;
        else
            TempData["ErrorMessage"] = result.Message;

        return RedirectToAction("PersonalInfo");
    }
}
