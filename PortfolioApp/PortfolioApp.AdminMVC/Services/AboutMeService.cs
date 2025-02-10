using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class AboutMeService : IAboutMeService
{
    private readonly IHttpClientFactory _factory;
    public AboutMeService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public Task<ServiceResult> CreateAboutMeAsync(AddAboutMeApiDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<AboutMeDto>> GetAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("aboutme/get");
        var result = await apiResponse.Content.ReadFromJsonAsync<ServiceResult<AboutMeDto>>();

        if (result is null)
            return new ServiceResult<AboutMeDto>(false);

        return result;

    }

    public Task<ServiceResult> UpdateAboutMeAsync(UpdateAboutMeApiDto dto)
    {
        throw new NotImplementedException();
    }
}
