using Mycinema.Domain.Entities;

namespace Mycinema.Application.Contracts.Repositories;

public interface IMovieGenreReadRepository
{
    Task<IReadOnlyList<MovieGenre>> GetAllAsync();
    Task<IReadOnlyList<MovieGenre>> GetByMovieIdAsync(int movieId);
    Task<IReadOnlyList<MovieGenre>> GetByGenreIdAsync(int genreId);
    Task<MovieGenre> GetByGenreIdAndMovieIdAsync(int genreId, int movieId);
}
