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
            return Redirect("/BlogPost/All");
        }

        var result = await _blogPostService.GetByIdWebAsync(id);

        if (!result.IsSuccess)
        {
            TempData["ErrorMessage"] = result.Message;
            return RedirectToAction(nameof(All));
        }

        var blogPostDto = result.Data;

        var blogPostModel = new BlogPostViewModel
        {
            Id = blogPostDto.Id,
            Content = blogPostDto.Content,
            PublishDate = blogPostDto.PublishDate,
            Title = blogPostDto.Title,
        };

        var commentModels = _mapper.Map<List<CommentViewModel>>(blogPostDto.Comments);

        blogPostModel.Comments = commentModels;

        return View(blogPostModel);
    }
  
    [HttpGet]
    public async Task<IActionResult> All()
    {
        var result = await _blogPostService.GetAllAsync();

        var dtos = result.Data;

        var models = _mapper.Map<List<AllBlogPostsViewModel>>(dtos);

        var filteredModels = models.Where(m => m.IsVisible).ToList();

        return View(filteredModels);
    }
}
