using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Interfaces;
using PortfolioApp.WebMVC.Models.ViewModels;

namespace PortfolioApp.WebMVC.Controllers;
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromForm] AddCommentViewModel model)
    {
        var dto = _mapper.Map<AddCommentDto>(model);

        var result =await _commentService.AddAsync(dto);

        if (result.IsSuccess)
        {
            TempData["Message"] = result.Message;
        }
        else
        {
            TempData["ErrorMessage"] = result.Message;
        }

        return Redirect($"/BlogPost/BlogPost/{model.BlogPostId}");
    }
}
