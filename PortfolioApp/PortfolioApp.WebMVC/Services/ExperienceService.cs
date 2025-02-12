using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services;
public class ExperienceService : IExperienceService
{
    private readonly IHttpClientFactory _factory;
    public ExperienceService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public Task<ServiceResult> AddAsync(AddExperienceDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<ExperienceDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("experience/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<ExperienceDto>>>();
    }

    public Task<ServiceResult<ExperienceDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateAsync(UpdateExperienceDto dto)
    {
        throw new NotImplementedException();
    }
}
