using Mycinema.Application.Models.DTOs;

namespace Mycinema.Application.Contracts.Infrastructure;

public interface IHttpClientService
{
    Task<PagedDto<TmdbMovieDto>> DiscoverMovies(int numberOfTheathers, DateTime startDateTime, DateTime endDateTime);
    Task<PagedDto<TmdbTvShowDto>> DiscoverTvShows(int numberOfScreens, DateTime startDateTime, DateTime endDateTime);
}
