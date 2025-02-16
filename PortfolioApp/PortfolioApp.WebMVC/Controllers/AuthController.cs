using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Controllers;
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

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromForm] RegisterViewModel registerModel)
	{
		var registerDto = _mapper.Map<RegisterDto>(registerModel);

        var result = await _authService.RegisterAsync(registerDto);

		if (!result.IsSuccess)
		{
			ViewData["ErrorMessage"] = result.Message;
			return View(registerModel);
		}

        TempData["Message"] = result.Message;
        return RedirectToAction(nameof(Login));
    }

	[HttpGet]
	public async Task<IActionResult> VerifyEmail([FromQuery] string email, string token)
	{

		var dto = new VerifyEmailDto(email, token);

		var result = await _authService.VerifyEmailAsync(dto);
			
		if(!result.IsSuccess)
			TempData["ErrorMessage"] = result.Message;
		else
			TempData["Message"] = result.Message;

		return RedirectToAction(nameof(Login));

	}

	[HttpGet]
	public IActionResult ForgotPassword()
	{
		return View();
	}

	[HttpPost]
	public async Task<IActionResult> ForgotPassword([FromForm] ForgotPasswordViewModel model)
	{
		var dto = _mapper.Map<ForgotPasswordDto>(model);

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
}
