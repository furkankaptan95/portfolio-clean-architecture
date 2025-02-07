using MediatR;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.BlogPost;

namespace PortfolioApp.Application.Use_Cases.BlogPost.Queries;
public class GetBlogPostsQuery : IRequest<ServiceResult<List<BlogPostDto>>> { }

