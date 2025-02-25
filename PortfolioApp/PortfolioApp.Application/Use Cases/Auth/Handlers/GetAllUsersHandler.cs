using AutoMapper;
using MediatR;
using PortfolioApp.Application.Use_Cases.Auth.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Auth;
using PortfolioApp.Core.Interfaces.Repositories;

namespace PortfolioApp.Application.Use_Cases.Auth.Handlers;
public class GetAllUsersHandler : IRequestHandler<GetAllUsersQuery, ServiceResult<List<AllUsersDto>>>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public GetAllUsersHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<ServiceResult<List<AllUsersDto>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetAllAsync();

        var dtos = _mapper.Map<List<AllUsersDto>>(userEntities);

        return new ServiceResult<List<AllUsersDto>>(true, null, dtos);
    }
}
