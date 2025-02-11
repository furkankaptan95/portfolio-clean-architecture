using AutoMapper;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class BlogPostService : IBlogPostService
{
    private readonly IHttpClientFactory _factory;
    private readonly IMapper _mapper;
    public BlogPostService(IHttpClientFactory factory, IMapper mapper)
    {
        _factory = factory;
        _mapper = mapper;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public async Task<ServiceResult> AddBlogPostAsync(AddBlogPostDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("blogpost/create", dto);

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

    public Task<ServiceResult<List<BlogPostDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<BlogPostDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> UpdateAsync(UpdateBlogPostDto dto)
    {
        throw new NotImplementedException();
    }
}
