using MediatR;
using PortfolioApp.Application.Use_Cases.AboutMe.Commands;
using PortfolioApp.Application.Use_Cases.AboutMe.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class AboutMeService : IAboutMeService
{
    private readonly IMediator _mediator;
    public AboutMeService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ServiceResult> CreateAboutMeAsync(AddAboutMeApiDto dto)
    {
        var command = new CreateAboutMeCommand(dto);
        return await _mediator.Send(command);
    }

    public Task<ServiceResult> CreateAboutMeAsync(AddAboutMeMvcDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<Stream>> DownloadCvAsync(string cvUrl)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<AboutMeDto>> GetAsync()
    {
        var result = await _mediator.Send(new GetAboutMeQuery());
        return result;
    }

    public async Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeApiDto dto)
    {
        var result = await _mediator.Send(new UpdateAboutMeCommand(dto));
        return result;
    }

    public Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeMVCDto dto)
    {
        throw new NotImplementedException();
    }
}
