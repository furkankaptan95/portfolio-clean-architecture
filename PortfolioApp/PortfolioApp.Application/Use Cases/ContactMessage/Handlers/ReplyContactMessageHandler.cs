using MediatR;
using Microsoft.EntityFrameworkCore;
using PortfolioApp.Application.Use_Cases.ContactMessage.Commands;
using PortfolioApp.Core.Common;
using PortfolioApp.Core.DTOs.Email;
using PortfolioApp.Infrastructure.Persistence.DbContexts;
using System.Net.Http.Json;

namespace PortfolioApp.Application.Use_Cases.ContactMessage.Handlers;
public class ReplyContactMessageHandler : IRequestHandler<ReplyContactMessageCommand, ServiceResult>
{
    private readonly IHttpClientFactory _factory;
    private readonly DataDbContext _dataDbContext;
    public ReplyContactMessageHandler(DataDbContext dataDbContext, IHttpClientFactory factory)
    {
        _dataDbContext = dataDbContext;
        _factory = factory;
    }

    private HttpClient EmailApiClient => _factory.CreateClient("emailApi");
    public async Task<ServiceResult> Handle(ReplyContactMessageCommand request, CancellationToken cancellationToken)
    {
        var entity = await _dataDbContext.ContactMessages.FirstOrDefaultAsync(x => x.Id == request.ReplyDto.Id);

        if (entity is null)
        {
            return new ServiceResult(false,"Mesaj bulunamadı.");
        }

        entity.IsRead = true;
        entity.Reply = request.ReplyDto.ReplyMessage;
        entity.ReplyDate = DateTime.UtcNow;

        _dataDbContext.ContactMessages.Update(entity);
        await _dataDbContext.SaveChangesAsync(cancellationToken);

        var htmlMailBody = $"<h1>Merhaba, Ben Furkan!</h1><p>{entity.Reply}</p>";

        var emailRequest = new EmailRequestDto
        {
            Body = htmlMailBody,
            Subject = "Benimle iletişime geçtiğiniz için teşekür ederim. İşte yanıtım!",
            To = entity.Email,
        };

        var emailResult = await EmailApiClient.PostAsJsonAsync("send", emailRequest);

        return new ServiceResult(true, "Mesaj başarıyla yanıtlandı.");
    }
}
