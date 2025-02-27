using Microsoft.EntityFrameworkCore;
using PortfolioApp.Core.DTOs.Admin.Home;
using PortfolioApp.Core.Interfaces.Repositories;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Infrastructure.Persistence.Repositories;
public class HomeRepository : IHomeRepository
{
    private readonly DataDbContext _dataDbContext;
    public HomeRepository(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }

    public async Task<HomeDto> GetHomeInfosAsync()
    {
        var dto = new HomeDto();
        dto.ContactMessagesCount = await _dataDbContext.ContactMessages.CountAsync();
        dto.CommentsCount = await _dataDbContext.Comments.CountAsync();
        dto.ProjectsCount = await _dataDbContext.Projects.CountAsync();
        dto.BlogPostsCount = await _dataDbContext.BlogPosts.CountAsync();
        dto.ExperiencesCount = await _dataDbContext.Experiences.CountAsync();
        dto.EducationsCount = await _dataDbContext.Educations.CountAsync();

        return dto;
    }
}
