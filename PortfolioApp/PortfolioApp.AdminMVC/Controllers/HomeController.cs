using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Home;
using PortfolioApp.AdminMVC.Services;

namespace PortfolioApp.AdminMVC.Controllers;

[Authorize(Roles = "Admin")]
public class HomeController(HomeService homeService,IMapper mapper) : Controller
{
    [AllowAnonymous]
    [HttpGet]
    public IActionResult Error()
    {
        return View();
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var result = await homeService.GetHomeInfosAsync();

        var model = mapper.Map<HomeViewModel>(result.Data);

        return View(model);
    }

}
