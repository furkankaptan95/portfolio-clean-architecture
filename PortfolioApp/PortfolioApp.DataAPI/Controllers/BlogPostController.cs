using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BlogPostController : ControllerBase
{
    private readonly IBlogPostService _blogPostService;
    public BlogPostController(IBlogPostService blogPostService)
    {
        _blogPostService = blogPostService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddBlogPostDto dto)
    {
        var result = await _blogPostService.AddBlogPostAsync(dto);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _blogPostService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _blogPostService.DeleteAsync(id);

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateBlogPostDto dto)
    {
        var result = await _blogPostService.UpdateAsync(dto);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _blogPostService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("visibility/{id:int}")]
    public async Task<IActionResult> Visibility([FromRoute] int id)
    {
        var result = await _blogPostService.ChangeVisibilityAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
    
}
