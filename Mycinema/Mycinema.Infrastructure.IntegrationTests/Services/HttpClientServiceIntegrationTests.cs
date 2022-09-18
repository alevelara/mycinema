using Moq;
using Mycinema.Application.Models;
using Mycinema.Infrastructure.Services;
using System;
using Xunit;

namespace Mycinema.Infrastructure.IntegrationTests.Services;

public class HttpClientServiceIntegrationTests
{
    public TmdbSettings _settings { get; set; }
    public HttpClientFactory _httpClientFactory{ get; set; }

    public HttpClientServiceIntegrationTests()
    {
        _settings = new TmdbSettings()
        {
            ApiKey = "f70a1748885aaae8053003cca36baaca",
            UrlBase = "https://api.themoviedb.org/3/"
        };

        var httpClientFactory = new Mock<IHttpClientFactory>();
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri(_settings.UrlBase);        
        httpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _httpClientFactory = new HttpClientFactory(httpClientFactory.Object);
    }

    [Fact]
    public async Task GivenValidParams_WhenDiscoverMoviesFromAPI_ThenReturnMovies()
    {
        var service = new HttpClientService(_settings, _httpClientFactory);
        var numberOfTheatres = 1;
        var startTime = DateTime.Now;
        var endTime = DateTime.Now.AddDays(7);
        var discoveredMovies = await service.DiscoverMovies(numberOfTheatres, startTime, endTime);

        Assert.NotNull(discoveredMovies);
    }

    [Fact]
    public async Task GivenValidParams_WhenDiscoverTvShowsFromAPI_ThenReturnTvShows()
    {
        var service = new HttpClientService(_settings, _httpClientFactory);
        var numberOfTheatres = 1;
        var startTime = DateTime.Now;
        var endTime = DateTime.Now.AddDays(7);
        var discoveredTvShows = await service.DiscoverTvShows(numberOfTheatres, startTime, endTime);

        Assert.NotNull(discoveredTvShows);
    }

}
