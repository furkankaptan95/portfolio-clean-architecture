﻿using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class ExperienceService : IExperienceService
{
    private readonly IHttpClientFactory _factory;
    public ExperienceService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public async Task<ServiceResult> AddAsync(AddExperienceDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("experience/create", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<ExperienceDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<ExperienceDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> UpdateAsync(UpdateExperienceDto dto)
    {
        throw new NotImplementedException();
    }
}
