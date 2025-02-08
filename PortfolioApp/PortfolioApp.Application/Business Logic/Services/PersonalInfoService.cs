using MediatR;
using PortfolioApp.Application.Use_Cases.PersonalInfo.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.Application.Business_Logic.Services;
public class PersonalInfoService : IPersonalInfoService
{
    private readonly IMediator _mediator;
    public PersonalInfoService(IMediator mediator)
    {
        _mediator = mediator;
    }
    public async Task<ServiceResult> AddAsync(AddPersonalInfoDto dto)
    {
        var result = await _mediator.Send(new CreatePersonalInfoCommand(dto));

        return result;
    }
}
