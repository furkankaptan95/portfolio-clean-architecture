using AutoMapper;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Core.DTOs.Admin.Project;
using PortfolioApp.Core.DTOs.File;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class ProjectService : IProjectService
{
    private readonly IHttpClientFactory _factory;
    private readonly IMapper _mapper;
    public ProjectService(IHttpClientFactory factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    private HttpClient FileApiClient => _factory.CreateClient("fileApi");
    public async Task<ServiceResult> AddAsync(AddApiProjectDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> AddAsync(AddMvcProjectDto dto)
    {
        var imageResponse = await FileApiClient.PostAsync("upload", content: new MultipartFormDataContent
          {
             { new StreamContent(dto.ImageFile.OpenReadStream()), "file", dto.ImageFile.FileName }
         });

        var cvResult = await imageResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();

        var apiDto = _mapper.Map<AddApiProjectDto>(dto);

        apiDto.ImageUrl = cvResult.Data.FileName;

        var dataApiResponse = await DataApiClient.PostAsJsonAsync("project/create", apiDto);

        return await dataApiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"project/visibility/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"project/delete/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public async Task<ServiceResult<List<ProjectDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("project/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<ProjectDto>>>();
    }

    public async Task<ServiceResult<ProjectDto>> GetByIdAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"project/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<ProjectDto>>();
    }

    public async Task<ServiceResult> UpdateAsync(UpdateProjectApiDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> UpdateAsync(UpdateProjectMVCDto dto)
    {
        var dataApiDto = _mapper.Map<UpdateProjectApiDto>(dto);

        if (dto.ImageFile is not null)
        {
            var cvResponse = await FileApiClient.PostAsync("upload", content: new MultipartFormDataContent
              {
                 { new StreamContent(dto.ImageFile.OpenReadStream()), "file", dto.ImageFile.FileName }
             });

            var cvResult = await cvResponse.Content.ReadFromJsonAsync<ServiceResult<FileNameDto>>();

            dataApiDto.ImageUrl = cvResult.Data.FileName;
        }

        var dataApiResponse = await DataApiClient.PostAsJsonAsync("project/update", dataApiDto);

        return await dataApiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }
}
