﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class ExperienceController : ControllerBase
{
    private readonly IExperienceService _experienceService;

    public ExperienceController(IExperienceService experienceService)
    {
        _experienceService = experienceService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddExperienceDto dto)
    {
        var result = await _experienceService.AddAsync(dto);

        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _experienceService.GetAllAsync();

        return Ok(result);
    }

    [HttpGet("delete/{id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var result = await _experienceService.DeleteAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdateExperienceDto dto)
    {
        var result = await _experienceService.UpdateAsync(dto);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get([FromRoute] int id)
    {
        var result = await _experienceService.GetByIdAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpGet("visibility/{id:int}")]
    public async Task<IActionResult> Visibility([FromRoute] int id)
    {
        var result = await _experienceService.ChangeVisibilityAsync(id);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
