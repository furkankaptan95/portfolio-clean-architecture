using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;
using PortfolioApp.Core.DTOs.Admin.Education;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services
{
    public class BlogPostService : IBlogPostService
    {
        private readonly IHttpClientFactory _factory;
        public BlogPostService(IHttpClientFactory factory)
        {
            _factory = factory;
        }
        private HttpClient DataApiClient => _factory.CreateClient("dataApi");
        public Task<ServiceResult> AddBlogPostAsync(AddBlogPostDto dto)
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

        public async Task<ServiceResult<List<BlogPostDto>>> GetAllAsync()
        {
            var apiResponse = await DataApiClient.GetAsync("blogpost/all");

            return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<BlogPostDto>>>();
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
}
