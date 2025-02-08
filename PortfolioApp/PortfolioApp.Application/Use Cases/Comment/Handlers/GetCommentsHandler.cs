﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.Comment.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.Comment.Handlers;
public class GetCommentsHandler : IRequestHandler<GetCommentsQuery, ServiceResult<List<CommentDto>>>
{
    private readonly DataDbContext _dataDbContext;
    public GetCommentsHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<List<CommentDto>>> Handle(GetCommentsQuery request, CancellationToken cancellationToken)
    {
        var dtos = new List<CommentDto>();

        var entities = await _dataDbContext.Comments.Include(c => c.BlogPost).ToListAsync();

        if (entities is null)
        {
            return new ServiceResult<List<CommentDto>>(true,"Yorum bulunmuyor.",dtos);
        }

        foreach (var item in entities)
        {
            var dto = new CommentDto
            {
                Id = item.Id,
                Content = item.Content,
                CreatedAt = item.CreatedAt,
                IsApproved = item.IsApproved,
                BlogPostName = item.BlogPost != null ? item.BlogPost.Title : "Blog Post silindi.",
                Commenter = item.SignedCommenterName ?? item.UnsignedCommenterName,
                CommenterId = item.UserId,
            };

            dtos.Add(dto);
        }

        return new ServiceResult<List<CommentDto>>(true,null, dtos);
    }
}
