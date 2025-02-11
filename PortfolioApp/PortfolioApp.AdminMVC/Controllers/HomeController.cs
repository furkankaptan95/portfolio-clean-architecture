using Microsoft.AspNetCore.Mvc;

namespace PortfolioApp.AdminMVC.Controllers;
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

}
