using Moq;
using Mycinema.Application.Models.DTOs;
using Mycinema.Infrastructure.Services;
using System.Net.Http.Json;
using Xunit;
using static System.Net.WebRequestMethods;

namespace Mycinema.Infrastructure.IntegrationTests.Services;

public class HttpClientFactoryIntegrationTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactory;
    private const string uri = "https://api.themoviedb.org/3";

    public HttpClientFactoryIntegrationTests()
    {
        _httpClientFactory = new Mock<IHttpClientFactory>();
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(uri);
        _httpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(httpClient);
    }

    [Fact]
    public async Task asdasd()
    {
        var httpClientFactory = new HttpClientFactory(_httpClientFactory.Object);
        var requestParam = uri + "/discover/movie?api_key=f70a1748885aaae8053003cca36baaca";

        var response = await httpClientFactory.GetAsync(requestParam, CancellationToken.None);
        var results = response.Content.ReadFromJsonAsync<PagedDto<TmdbMovieDto>>();

        Assert.NotNull(response);

    }


}
