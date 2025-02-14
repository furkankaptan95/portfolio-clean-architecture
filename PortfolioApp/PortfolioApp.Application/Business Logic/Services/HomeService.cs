using MediatR;
using PortfolioApp.Application.Use_Cases.Home.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Home;

namespace PortfolioApp.Application.Business_Logic.Services;
public class HomeService
{
    private readonly IMediator _mediator;
    public HomeService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ServiceResult<HomeDto>> GetHomeInfosAsync()
    {
        var result = await _mediator.Send(new GetHomeInfosQuery());
        return result;
    }
}
