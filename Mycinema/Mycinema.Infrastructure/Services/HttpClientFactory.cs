using Microsoft.Extensions.Logging;
using Mycinema.Application.Contracts.Infrastructure;
using System.Net.Http;

namespace Mycinema.Infrastructure.Services;

public class HttpClientFactory : IHttpClient
{
    private readonly IHttpClientFactory _clientFactory;
    private readonly ILogger<HttpClientFactory> _logger;
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
            _logger.LogError($"There was an error accessing to Tmdb API: {e.Message}");
            return null;
        }        
    }
}
