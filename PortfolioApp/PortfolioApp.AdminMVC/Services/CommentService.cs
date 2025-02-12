using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Core.DTOs.Web.Comment;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Services;
public class CommentService : ICommentService
{
    private readonly IHttpClientFactory _factory;
    public CommentService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public Task<ServiceResult> AddAsync(AddCommentDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> ApprovalAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<CommentDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("comment/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<CommentDto>>>();
    }
}
