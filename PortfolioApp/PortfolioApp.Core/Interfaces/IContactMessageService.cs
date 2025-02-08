using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Admin.ContactMessage;
using PortfolioApp.Core.DTOs.Web.ContactMessage;

namespace PortfolioApp.Core.Interfaces;
public interface IContactMessageService
{
    Task<ServiceResult> AddAsync(AddContactMessageDto dto); 
    Task<ServiceResult<List<ContactMessageDto>>> GetAllAsync();
    Task<ServiceResult<ContactMessageDto>> GetByIdAsync(int id);
    Task<ServiceResult> ReplyAsync(ReplyContactMessageDto dto);
}
