using AutoMapper;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services;
public class AboutMeService : IAboutMeService
{
    private readonly IHttpClientFactory _factory;
    private readonly IMapper _mapper;
    public AboutMeService(IHttpClientFactory factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public Task<ServiceResult> CreateAboutMeAsync(AddAboutMeApiDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> CreateAboutMeAsync(AddAboutMeMvcDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<AboutMeDto>> GetAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("aboutme/get");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<AboutMeDto>>();
    }

    public Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeApiDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeMVCDto dto)
    {
        throw new NotImplementedException();
    }
}
