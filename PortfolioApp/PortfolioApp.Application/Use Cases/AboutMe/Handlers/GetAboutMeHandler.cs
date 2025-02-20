﻿using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.AboutMe.Queries;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.AboutMe;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.AboutMe.Handlers;
public class GetAboutMeHandler : IRequestHandler<GetAboutMeQuery, ServiceResult<AboutMeDto>>
{
    private readonly DataDbContext _dataDbContext;
    public GetAboutMeHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult<AboutMeDto>> Handle(GetAboutMeQuery request, CancellationToken cancellationToken)
    {
        var aboutMeEntity = await _dataDbContext.AboutMe.FirstOrDefaultAsync();

        if (aboutMeEntity is null)
        {
            return new ServiceResult<AboutMeDto>(false, "Hakkımda kısmına henüz bir şey eklemediniz.");
        }

        var aboutMeDto = new AboutMeDto
        {
            FullName = aboutMeEntity.FullName,
            Field = aboutMeEntity.Field,
            Introduction = aboutMeEntity.Introduction,
            AboutMeImageUrl = aboutMeEntity.AboutMeImageUrl,
            LinkedInUrl = aboutMeEntity.LinkedInUrl,
            GithubUrl = aboutMeEntity.GithubUrl,
            Email = aboutMeEntity.Email,
            CvUrl = aboutMeEntity.CvUrl,
        };

        return new ServiceResult<AboutMeDto>(true,null,aboutMeDto);
    }
}
