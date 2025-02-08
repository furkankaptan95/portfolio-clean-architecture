﻿using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Experience;
using PortfolioApp.Core.DTOs.Admin.Project;

namespace PortfolioApp.Core.Interfaces;
public interface IProjectService
{
    Task<ServiceResult> AddAsync(AddProjectDto dto);
}
