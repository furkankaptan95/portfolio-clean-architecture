using MediatR;
using PortfolioApp.Application.Use_Cases.BlogPost.Commands;
using PortfolioApp.Application.Use_Cases.Education.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
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
}
