using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class EducationService : IEducationService
{
    private readonly IHttpClientFactory _factory;
    public EducationService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public async Task<ServiceResult> AddAsync(AddEducationDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("education/create", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"education/visibility/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public Task<ServiceResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<EducationDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("education/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<EducationDto>>>();
    }

    public async Task<ServiceResult<EducationDto>> GetByIdAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"education/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<EducationDto>>();
    }

    public async Task<ServiceResult> UpdateAsync(UpdateEducationDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("education/update", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }
}
