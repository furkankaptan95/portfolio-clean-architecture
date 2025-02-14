using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Home;
using PortfolioApp.AdminMVC.Services;
using PortfolioApp.Core.DTOs.Admin.Home;

namespace PortfolioApp.AdminMVC.Controllers;
public class HomeController(HomeService homeService,IMapper mapper) : Controller
{
    public async Task<IActionResult> Index()
    {
        var result = await homeService.GetHomeInfosAsync();

        var model = mapper.Map<HomeViewModel>(result.Data);

        return View(model);
    }
}
