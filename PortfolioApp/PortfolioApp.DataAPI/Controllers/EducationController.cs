﻿using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Application.Business_Logic.Services;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EducationController : ControllerBase
{
    private readonly IEducationService _educationService;
    public EducationController(IEducationService educationService)
    {
        _educationService = educationService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddEducationDto dto)
    {
        var result = await _educationService.AddAsync(dto);

        return Ok(result);
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAll()
    {
        var result = await _educationService.GetAllAsync();

        return Ok(result);
    }
}
