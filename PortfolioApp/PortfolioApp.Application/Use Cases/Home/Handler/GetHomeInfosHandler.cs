using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Home.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Home;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Home.Handler;
public class GetHomeInfosHandler : IRequestHandler<GetHomeInfosQuery, ServiceResult<HomeDto>>
{
    private readonly DataDbContext _dataDbContext;
    public GetHomeInfosHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<HomeDto>> Handle(GetHomeInfosQuery request, CancellationToken cancellationToken)
    {
        var dto = new HomeDto();

        dto.CommentsCount = await _dataDbContext.Comments.CountAsync();
        dto.ProjectsCount = await _dataDbContext.Projects.CountAsync();
        dto.BlogPostsCount = await _dataDbContext.BlogPosts.CountAsync();
        dto.ExperiencesCount = await _dataDbContext.Experiences.CountAsync();
        dto.EducationsCount = await _dataDbContext.Educations.CountAsync();

        return new ServiceResult<HomeDto>(true, null, dto);
    }
}
