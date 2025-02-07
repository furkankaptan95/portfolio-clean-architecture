﻿using Microsoft.AspNetCore.Http;
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

    [HttpGet("delete/{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _blogPostService.DeleteAsync(id);

        return Ok(result);
    }
}
