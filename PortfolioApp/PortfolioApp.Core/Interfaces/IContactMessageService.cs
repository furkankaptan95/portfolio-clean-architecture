﻿using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Web.ContactMessage;

namespace PortfolioApp.Core.Interfaces;
public interface IContactMessageService
{
    Task<ServiceResult> AddAsync(AddContactMessageDto dto);
}
