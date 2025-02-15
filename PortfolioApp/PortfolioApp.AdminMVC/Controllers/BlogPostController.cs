using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;

[Authorize(Roles = "Admin")]
public class BlogPostController : Controller
{
    private readonly IBlogPostService _blogPostService;
    private readonly IMapper _mapper;
    public BlogPostController(IMapper mapper, IBlogPostService blogPostService)
    {
        _blogPostService = blogPostService;
        _mapper = mapper;
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] AddBlogPostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<AddBlogPostDto>(model);

        var result = await _blogPostService.AddBlogPostAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        ViewData["ErrorMessage"] = result.Message;
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _blogPostService.GetAllAsync();

        var models = _mapper.Map<List<BlogPostViewModel>>(result.Data);

        return View(models);
    }

    [HttpGet]
    public async Task<IActionResult> Update([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _blogPostService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        var model = _mapper.Map<UpdateBlogPostViewModel>(result.Data);

        return View(model);
    }

    [HttpPost]
    public async Task<IActionResult> Update([FromForm] UpdateBlogPostViewModel model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var dto = _mapper.Map<UpdateBlogPostDto>(model);

        var result = await _blogPostService.UpdateAsync(dto);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Visibility([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _blogPostService.ChangeVisibilityAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }

    [HttpGet]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _blogPostService.DeleteAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }
}
