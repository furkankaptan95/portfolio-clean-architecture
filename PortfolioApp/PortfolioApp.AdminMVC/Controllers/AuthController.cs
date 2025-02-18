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
		if (!ModelState.IsValid)
		{
			return View(loginmodel);
		}

		var loginDto = _mapper.Map<LoginDto>(loginmodel);

		loginDto.IsAdmin = true;

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

    [HttpGet]
    public IActionResult ForgotPassword()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<ForgotPasswordDto>(model);

		dto.IsAdmin = true;

        var result = await _authService.ForgotPasswordAsync(dto);

        if (!result.IsSuccess)
        {
            ViewData["ErrorMessage"] = result.Message;
            return View(model);
        }

        ViewData["Message"] = result.Message;
        return View();
    }


	[HttpGet]
	public async Task<IActionResult> RenewPassword([FromQuery] string email, string token)
	{

		var dto = new RenewPasswordDto(email, token);

		var result = await _authService.RenewPasswordVerifyEmailAsync(dto);

		if (!result.IsSuccess)
		{
			TempData["ErrorMessage"] = result.Message;
			return RedirectToAction(nameof(ForgotPassword));
		}

		ViewData["Message"] = result.Message;

		var model = new NewPasswordViewModel
		{
			Token = result.Data,
			Email = email
		};

		return View(model);
	}

    [HttpPost]
    public async Task<IActionResult> RenewPassword([FromForm] NewPasswordViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<NewPasswordDto>(model);

        var result = await _authService.NewPasswordAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(Login));
        }

        TempData["ErrorMessage"] = result.Message;
        return RedirectToAction(nameof(ForgotPassword));
    }

    [HttpGet]
	public async Task<IActionResult> LogOut()
	{
		var refreshToken = Request.Cookies["RefreshToken"];

		if (string.IsNullOrEmpty(refreshToken))
		{
			Response.Cookies.Delete("JwtToken");

			TempData["Message"] = "Hesabınızdan başarıyla çıkış yapıldı.";
			return Redirect("/Auth/Login");

		}

		var result = await _authService.RevokeTokenAsync(refreshToken);

		if (result.IsSuccess)
		{
			Response.Cookies.Delete("JwtToken");
			Response.Cookies.Delete("RefreshToken");

			TempData["Message"] = "Hesabınızdan başarıyla çıkış yapıldı.";
			return Redirect("/Auth/Login");
		}

		TempData["ErrorMessage"] = "Hesabınızdan çıkış yapılırken bir problem oluştu..";
		return Redirect("/");
	}
}
