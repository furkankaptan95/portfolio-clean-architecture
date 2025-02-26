﻿using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Application.Use_Cases.Education.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class EducationService : IEducationService
{
    private readonly IMediator _mediator;
    public EducationService(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task<ServiceResult> AddAsync(AddEducationDto dto)
    {
        var result = await _mediator.Send(new CreateEducationCommand(dto));

        return result;
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        var result = await _mediator.Send(new EducationVisibilityCommand(id));

        return result;
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var result = await _mediator.Send(new DeleteEducationCommand(id));

        return result;
    }

    public async Task<ServiceResult<List<EducationDto>>> GetAllAsync()
    {
        var result = await _mediator.Send(new GetEducationsQuery());

        return result;
    }

    public async Task<ServiceResult<EducationDto>> GetByIdAsync(int id)
    {
        var result = await _mediator.Send(new GetEducationByIdQuery(id));

        return result;
    }

    public async Task<ServiceResult> UpdateAsync(UpdateEducationDto dto)
    {
        var result = await _mediator.Send(new UpdateEducationCommand(dto));

        return result;
    }
}
