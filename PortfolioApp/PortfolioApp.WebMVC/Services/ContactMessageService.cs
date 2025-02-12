using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Core.DTOs.Web.ContactMessage;
using PortfolioApp.Core.Interfaces;

namespace PortfolioApp.WebMVC.Services;
public class ContactMessageService : IContactMessageService
{
    private readonly IHttpClientFactory _factory;
    public ContactMessageService(IHttpClientFactory factory)
    {
        _factory = factory;
    }
    private HttpClient DataApiClient => _factory.CreateClient("dataApi");

    public async Task<ServiceResult> AddAsync(AddContactMessageDto dto)
    {
        var apiResponse = await DataApiClient.PostAsJsonAsync("ContactMessage/create", dto);

        return await apiResponse.Content.ReadFromJsonAsync<ServiceResult>();
    }

    public Task<ServiceResult<List<ContactMessageDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult<ContactMessageDto>> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> MakeReadAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<ServiceResult> ReplyAsync(ReplyContactMessageDto dto)
    {
        throw new NotImplementedException();
    }
}
