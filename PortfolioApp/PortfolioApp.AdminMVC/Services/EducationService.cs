using PortfolioApp.Core.Common;
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

    public Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<List<EducationDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
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
