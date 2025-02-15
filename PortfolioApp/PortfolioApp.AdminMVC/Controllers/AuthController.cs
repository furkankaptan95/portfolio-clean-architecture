using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Auth;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
public class AuthController : Controller
{
    private readonly IAuthService _authService;
	private readonly IMapper _mapper;
	public AuthController(IAuthService authService, IMapper mapper)
    {
        _authService = authService;
		_mapper = mapper;
    }
    public IActionResult Forbidden()
    {
        return View();
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

	[HttpPost] 
	public async Task<IActionResult> Login([FromForm] LoginViewModel loginmodel)
	{
		var loginDto = _mapper.Map<LoginDto>(loginmodel);

		var result = await _authService.LoginAsync(loginDto);

		if (!result.IsSuccess)
		{
			ViewData["ErrorMessage"] = result.Message;
			return View(loginmodel);
		}

		CookieOptions jwtCookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Secure = true,
			SameSite = SameSiteMode.Strict,
			Expires = DateTime.UtcNow.AddMinutes(10)
		};

		CookieOptions refreshTokenCookieOptions = new CookieOptions
		{
			HttpOnly = true,
			Secure = true,
			SameSite = SameSiteMode.Strict,
			Expires = DateTime.UtcNow.AddDays(7)
		};

		HttpContext.Response.Cookies.Append("JwtToken", result.Data.JwtToken, jwtCookieOptions);
		HttpContext.Response.Cookies.Append("RefreshToken", result.Data.RefreshToken, refreshTokenCookieOptions);

		TempData["Message"] = result.Message;

		return Redirect("/");
	}
}
