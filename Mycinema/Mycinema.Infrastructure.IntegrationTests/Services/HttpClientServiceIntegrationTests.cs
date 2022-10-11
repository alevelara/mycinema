using Microsoft.Extensions.Options;
using Moq;
using Mycinema.Application.Models;
using Mycinema.Infrastructure.Services;
using Xunit;

namespace Mycinema.Infrastructure.IntegrationTests.Services;

public class HttpClientServiceIntegrationTests
{
    public IOptions<TmdbSettings> _settings { get; set; }
    public HttpClientFactory _httpClientFactory{ get; set; }

    public HttpClientServiceIntegrationTests()
    {
        var settings = new TmdbSettings()
        {
            ApiKey = "f70a1748885aaae8053003cca36baaca",
            UrlBase = "https://api.themoviedb.org/3/"
        };
        _settings = Options.Create(settings);

        var httpClientFactory = new Mock<IHttpClientFactory>();
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_settings!.Value.UrlBase);        
        httpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _httpClientFactory = new HttpClientFactory(httpClientFactory.Object);
    }

    [Fact]
    public async Task HttpClientService_ShouldDiscoverMoviesByDate()
    {
        var service = new HttpClientService(_settings, _httpClientFactory);
        var startTime = DateTime.Now;
        var endTime = DateTime.Now.AddDays(7);
        var discoveredMovies = await service.DiscoverMovies(startTime, endTime);

        Assert.NotNull(discoveredMovies);
    }

    [Fact]
    public async Task HttpClientService_ShouldDiscoverTvShowsByDate()
    {
        var service = new HttpClientService(_settings, _httpClientFactory);
        var startTime = DateTime.Now;
        var endTime = DateTime.Now.AddDays(7);
        var discoveredTvShows = await service.DiscoverTvShows( startTime, endTime);

        Assert.NotNull(discoveredTvShows);
    }
}
