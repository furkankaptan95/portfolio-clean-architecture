using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Services;

namespace PortfolioApp.WebMVC.Controllers;
public class HomeController : Controller
{
    private readonly HomeService _homeService;
    private readonly IAboutMeService _aboutMeService;
    public HomeController(HomeService homeService, IAboutMeService aboutMeService)
    {
        _aboutMeService = aboutMeService;
        _homeService = homeService;
    }
    public IActionResult Error()
    {
        return View();
    }
    public async Task<IActionResult> Index()
    {
        var model = await _homeService.GetHomeModel();
        
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DownloadCv([FromQuery] string cvUrl)
    {
        var result = await _aboutMeService.DownloadCvAsync(cvUrl);

        return new FileStreamResult(result.Data, "application/pdf")
        {
            FileDownloadName = "FurkanKaptanOzgecmis.pdf"
        };
    }
}
