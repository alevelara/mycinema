namespace Mycinema.Application.Contracts.Infrastructure;

public interface IHttpClient
{
    Task<HttpResponseMessage> GetAsync(string? requestParams, CancellationToken cancellationToken);
}
