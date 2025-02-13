using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Controllers;
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
    public async Task<IActionResult> BlogPost([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "BlogPost bulunamadı.";
            return Redirect("/all-blog-posts");
        }

        var model = new BlogPostViewModel
        {
            Id = id,
            Title = "Title",
            Content = "Content",
            PublishDate = DateTime.Now,
        };

        return View(model);
    }
}
