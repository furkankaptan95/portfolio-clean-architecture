using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddCommentDto dto)
    {
        var result = await _commentService.AddAsync(dto);

        return Ok(result);
    }
}
