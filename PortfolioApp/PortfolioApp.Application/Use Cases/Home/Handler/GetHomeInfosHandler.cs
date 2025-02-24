using MediatR;
using PortfolioApp.Application.Use_Cases.Home.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Home;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Home.Handler;
public class GetHomeInfosHandler : IRequestHandler<GetHomeInfosQuery, ServiceResult<HomeDto>>
{
    private readonly IHomeRepository _homeRepository;
    public GetHomeInfosHandler(IHomeRepository homeRepository)
    {
        _homeRepository = homeRepository;
    }
    public async Task<ServiceResult<HomeDto>> Handle(GetHomeInfosQuery request, CancellationToken cancellationToken)
    {
        var dto = await _homeRepository.GetHomeInfosAsync();

        return new ServiceResult<HomeDto>(true, null, dto);
    }
}
