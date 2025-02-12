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
}
