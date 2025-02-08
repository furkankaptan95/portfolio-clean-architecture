using MediatR;
using PortfolioApp.Application.Use_Cases.Experience.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class ExperienceService : IExperienceService
{
    private readonly IMediator _mediator;
    public ExperienceService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ServiceResult> AddAsync(AddExperienceDto dto)
    {
        var result = await _mediator.Send(new CreateExperienceCommand(dto));

        return result;

    }

}
