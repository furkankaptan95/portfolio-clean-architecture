using Microsoft.AspNetCore.Mvc;
using PortfolioApp.WebMVC.Services;

namespace PortfolioApp.WebMVC.Controllers;
public class HomeController : Controller
{
    private readonly HomeService _homeService;
    public HomeController(HomeService homeService)
    {
        _homeService = homeService;
    }
    public async Task<IActionResult> Index()
    {
        var model = await _homeService.GetHomeModel();
        
        return View(model);
    }
}
