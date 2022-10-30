
using Mycinema.Application.Models.DTOs;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Models.Entities;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Services.MovieRecommendations;

public interface IGetMovieRecommendation
{
    public Task<PagedDto<TmdbMovieDto>> GetMovieRecommendations(DateTime startDatetime, DateTime endDatetime);
    public Task<List<Movie>> GetMostSuccesfulMoviesFromDb(DateTime startDatetime, DateTime endDatetime);
}
