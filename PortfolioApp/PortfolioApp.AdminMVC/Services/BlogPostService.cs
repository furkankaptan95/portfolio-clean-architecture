using AutoMapper;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Web.BlogPost;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class BlogPostService : IBlogPostService
{
    private readonly IHttpClientFactory _factory;
    public BlogPostService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public async Task<ServiceResult> AddBlogPostAsync(AddBlogPostDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("blogpost/create", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
        
    }

    public async Task<ServiceResult> ChangeVisibilityAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"blogpost/visibility/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();

    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"blogpost/delete/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();

    }

    public async Task<ServiceResult<List<BlogPostDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("blogpost/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<BlogPostDto>>>();
    }

    public async Task<ServiceResult<BlogPostDto>> GetByIdAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"blogpost/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<BlogPostDto>>();

    }

    public Task<ServiceResult<BlogPostWebDto>> GetByIdWebAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> UpdateAsync(UpdateBlogPostDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("blogpost/update", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }
}
