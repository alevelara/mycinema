using Microsoft.Extensions.Logging;
using Mycinema.Application.Contracts.Infrastructure;

namespace Mycinema.Infrastructure.Services;

public class HttpClientFactory : IHttpClient
{
    private readonly IHttpClientFactory _clientFactory;

    public HttpClientFactory(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
    }

    public async Task<HttpResponseMessage> GetAsync(string? requestParam, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestParam);
        var client = _clientFactory.CreateClient("tmdb");
        try
        {
            return await client.SendAsync(request);
        } 
        catch  (Exception e)
        {
            return null;
        }
    }
}
