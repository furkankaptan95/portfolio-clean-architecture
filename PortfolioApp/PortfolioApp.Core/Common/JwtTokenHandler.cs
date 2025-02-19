using Microsoft.Extensions.Configuration;
using PortfolioApp.Core.Helpers;

namespace PortfolioApp.Core.Common;
public class JwtTokenHandler : DelegatingHandler
{
    private readonly IConfiguration _configuration;
    public JwtTokenHandler(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var jwtToken = TokenGenerator.GenerateJwtTokenWithoutClaims(_configuration);

        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", jwtToken);

        return await base.SendAsync(request, cancellationToken);
    }
}

