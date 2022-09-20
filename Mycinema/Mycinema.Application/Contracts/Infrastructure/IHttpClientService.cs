using Mycinema.Application.Models.DTOs;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;

namespace Mycinema.Application.Contracts.Infrastructure;

public interface IHttpClientService
{
    Task<PagedDto<TmdbMovieDto>> DiscoverMovies(DateTime startDateTime, DateTime endDateTime);
    Task<PagedDto<TmdbTvShowDto>> DiscoverTvShows(DateTime startDateTime, DateTime endDateTime);
}
