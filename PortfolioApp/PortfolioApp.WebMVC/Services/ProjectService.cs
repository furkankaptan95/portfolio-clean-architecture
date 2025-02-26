﻿using AutoMapper;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services;
public class ProjectService : IProjectService
{
    private readonly IHttpClientFactory _factory;
    public ProjectService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public Task<ServiceResult> AddAsync(AddApiProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> AddAsync(AddMvcProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> DeleteAsync(DeleteProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<ProjectDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("project/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<ProjectDto>>>();
    }

    public Task<ServiceResult<ProjectDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateAsync(UpdateProjectApiDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateAsync(UpdateProjectMVCDto dto)
    {
        throw new NotImplementedException();
    }
}
