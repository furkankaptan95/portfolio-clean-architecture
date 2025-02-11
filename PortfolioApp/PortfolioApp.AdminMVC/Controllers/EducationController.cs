using Microsoft.AspNetCore.Mvc;

namespace PortfolioApp.AdminMVC.Controllers;
public class EducationController : Controller
{
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }
}
