using MediatR;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.Entities;
using PortfolioApp.Infrastructure.Persistence.DbContexts;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class CreateContactMessageHandler : IRequestHandler<CreateContactMessageCommand, ServiceResult>
{
    private readonly DataDbContext _dataDbContext;
    public CreateContactMessageHandler(DataDbContext dataDbContext)
    {
        _dataDbContext = dataDbContext;
    }
    public async Task<ServiceResult> Handle(CreateContactMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = new ContactMessageEntity()
        {
            Name = request.ContactMessage.Name,
            Email = request.ContactMessage.Email,
            Subject = request.ContactMessage.Subject,
            Message = request.ContactMessage.Message,
        };

        await _dataDbContext.ContactMessages.AddAsync(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        return new ServiceResult(true, "Mesaj başarıyla gönderildi.");
    }
}
