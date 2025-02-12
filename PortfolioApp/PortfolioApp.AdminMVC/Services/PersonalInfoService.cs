using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.Admin.PersonalInfo;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class PersonalInfoService : IPersonalInfoService
{
    private readonly IHttpClientFactory _factory;
    public PersonalInfoService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public async Task<ServiceResult> AddAsync(AddPersonalInfoDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("personalinfo/create", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public async Task<ServiceResult<PersonalInfoDto>> GetAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("personalinfo/get");
        
        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<PersonalInfoDto>>();
    }

    public async Task<ServiceResult> UpdateAsync(UpdatePersonalInfoDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("personalinfo/update", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }
}
