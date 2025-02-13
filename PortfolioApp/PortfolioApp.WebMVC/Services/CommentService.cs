using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services;
public class CommentService : ICommentService
{
    private readonly IHttpClientFactory _factory;
    public CommentService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public async Task<ServiceResult> AddAsync(AddCommentDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("comment/create", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public Task<ServiceResult> ApprovalAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        var apiResponse = await DataApiClient.GetAsync($"comment/delete/{id}");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public Task<ServiceResult<List<CommentDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}
