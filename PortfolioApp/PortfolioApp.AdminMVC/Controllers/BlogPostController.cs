using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.BlogPost;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
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
        var dto = _mapper.Map<AddBlogPostDto>(model);

        var result = await _blogPostService.AddBlogPostAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
            return RedirectToAction("All");
        }

        ViewData["ErrorMessage"] = result.Message;
        return View(model);
    }
}
