using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.AdminMVC.Models.ViewModels.Comment;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Controllers;
public class CommentController : Controller
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;
    public CommentController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> All()
    {
        
        var result = await _commentService.GetAllAsync();
            
        var models = _mapper.Map<List<CommentViewModel>>(result.Data);

        return View(models);

    }

    [HttpGet]
    public async Task<IActionResult> Approval([FromRoute] int id)
    {
        if (id < 1)
        {
            TempData["ErrorMessage"] = "Geçersiz ID";
            return RedirectToAction(nameof(All));
        }

        var result = await _commentService.ApprovalAsync(id);

        if (!result.IsSuccess)
            TempData["ErrorMessage"] = result.Message;
        else
            TempData["Message"] = result.Message;

        return RedirectToAction(nameof(All));
    }
}
