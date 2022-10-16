using Microsoft.Extensions.Options;
using Mycinema.Application.Constants.Tmdb;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Exceptions;
using Mycinema.Application.Models;
using Mycinema.Application.Models.DTOs;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using System.Net.Http.Json;

namespace Mycinema.Infrastructure.Services;

public class HttpClientService : IHttpClientService
{
    private readonly TmdbSettings _tmdbSettings;
    private readonly IHttpClient _httpClientFactory;

    public HttpClientService(IOptions<TmdbSettings> tmdbSettings, IHttpClient httpClientFactory)
    {
        _tmdbSettings = tmdbSettings.Value;
        _httpClientFactory = httpClientFactory;
    }

    public async Task<PagedDto<TmdbMovieDto>> DiscoverMovies(DateTime startDateTime, DateTime endDateTime)
    {
        var request = $"{_tmdbSettings.UrlBase}{TmdbEndpoints.discoverMovie}?api_key={_tmdbSettings.ApiKey}&primary_release_date.gte={startDateTime.ToString("yyyy-MM-dd")}&primary_release_date.lte={endDateTime.ToString("yyyy-MM-dd")}&sort_by=vote_average.desc";
        try
        {
            var apiResponse = await _httpClientFactory.GetAsync(request, CancellationToken.None);
            if (!apiResponse.IsSuccessStatusCode)
            {
                return null;
            }

            return await apiResponse.Content!.ReadFromJsonAsync<PagedDto<TmdbMovieDto>>();
        }
        catch (Exception e)
        {
            throw new BadRequestException($"Something happened discovering Movies from Tmdb API: {e.Message}");
        }
    }

    public async Task<PagedDto<TmdbTvShowDto>> DiscoverTvShows(DateTime startDateTime, DateTime endDateTime)
    {
        var request = $"{_tmdbSettings.UrlBase}{TmdbEndpoints.discoverTvShow}?api_key={_tmdbSettings.ApiKey}&first_air_date.gte={startDateTime.ToString("yyyy-MM-dd")}&first_air_date.lte={endDateTime.ToString("yyyy-MM-dd")}&sort_by=popularity.desc";
        try
        {
            var apiResponse = await _httpClientFactory.GetAsync(request, CancellationToken.None);
            if (!apiResponse.IsSuccessStatusCode)
            {
                return null;
            }

            return await apiResponse.Content!.ReadFromJsonAsync<PagedDto<TmdbTvShowDto>>();
        }
        catch (Exception e)
        {
            throw new BadRequestException($"Something happened discovering TV Shows from Tmdb API: {e.Message}");
        }
    }
}
