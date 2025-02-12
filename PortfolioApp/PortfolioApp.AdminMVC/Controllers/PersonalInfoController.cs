using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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
}
