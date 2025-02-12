using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services;
public class EducationService : IEducationService
{
    private readonly IHttpClientFactory _factory;
    public EducationService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public Task<ServiceResult> AddAsync(AddEducationDto dto)
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

    public async Task<ServiceResult<List<EducationDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("education/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<EducationDto>>>();
    }

    public Task<ServiceResult<EducationDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateAsync(UpdateEducationDto dto)
    {
        throw new NotImplementedException();
    }
}
