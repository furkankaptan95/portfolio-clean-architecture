using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Home;

namespace PortfolioApp.AdminMVC.Services;
public class HomeService(IHttpClientFactory factory)
{
    private HttpClient DataApiClient => factory.CreateClient("dataApi");
    public async Task<ServiceResult<HomeDto>> GetHomeInfosAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("home/get");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<HomeDto>>();
    }
}

