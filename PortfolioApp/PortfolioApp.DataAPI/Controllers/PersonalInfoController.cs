﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.DataAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Admin")]
public class PersonalInfoController : ControllerBase
{
    private readonly IPersonalInfoService _personalInfoService;

    public PersonalInfoController(IPersonalInfoService personalInfoService)
    {
         _personalInfoService = personalInfoService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] AddPersonalInfoDto dto)
    {
        var result = await _personalInfoService.AddAsync(dto);

        if (!result.IsSuccess)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
    [AllowAnonymous]
    [HttpGet("get")]
    public async Task<IActionResult> Get()
    {
        var result = await _personalInfoService.GetAsync();

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update([FromBody] UpdatePersonalInfoDto dto)
    {
        var result = await _personalInfoService.UpdateAsync(dto);

        if (!result.IsSuccess)
        {
            return NotFound(result);
        }

        return Ok(result);
    }
}
