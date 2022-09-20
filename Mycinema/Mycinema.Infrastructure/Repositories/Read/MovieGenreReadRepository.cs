using Dapper;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Domain.Entities;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mycinema.Infrastructure.Repositories.Read;

public class MovieGenreReadRepository : IMovieGenreReadRepository
{
    private DbConnection _connection;

    private string _connectionString => _connection.ConnectionString;    

    public MovieGenreReadRepository(DbConnection connection)
    {
        _connection = connection;
    }

    public async Task<IReadOnlyList<MovieGenre>> GetAllAsync()
    {
        var sql = "SELECT * FROM MovieGenre";
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryAsync<MovieGenre>(sql);
            return result.ToList();
        }
    }

    public async Task<IReadOnlyList<MovieGenre>> GetByMovieIdAsync(int movieId)
    {
        var sql = $"SELECT * FROM MovieGenre WHERE MovieId = @MovieId";
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryAsync<MovieGenre>(sql, new { MovieId = movieId });
            return result.ToList();
        }
    }

    public async Task<IReadOnlyList<MovieGenre>> GetByGenreIdAsync(int genreId)
    {
        var sql = $"SELECT * FROM MovieGenre WHERE GenreId = @GenreId";
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryAsync<MovieGenre>(sql, new { GenreId = genreId });
            return result.ToList();
        }
    }

    public async Task<MovieGenre> GetByGenreIdAndMovieIdAsync(int genreId, int movieId)
    {
        var sql = $"SELECT * FROM MovieGenre WHERE GenreId = @GenreId AND MovieId = @MovieId";
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryFirstOrDefaultAsync<MovieGenre>(sql, new { GenreId = genreId, MovieId = movieId });
            return result;
        }
    }    
}
