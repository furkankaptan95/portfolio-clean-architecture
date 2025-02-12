﻿using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.Comment;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Core.DTOs.Web.ContactMessage;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.AdminMVC.Service;
public class ContactMessageService : IContactMessageService
{
    private readonly IHttpClientFactory _factory;
    public ContactMessageService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");
    public Task<ServiceResult> AddAsync(AddContactMessageDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult<List<ContactMessageDto>>> GetAllAsync()
    {
        var apiResponse = await DataApiClient.GetAsync("contactmessage/all");

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult<List<ContactMessageDto>>>();
    }

    public async Task<ServiceResult<ContactMessageDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ServiceResult> ReplyAsync(ReplyContactMessageDto dto)
    {
        throw new NotImplementedException();
    }
}
