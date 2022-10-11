using Dapper;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Domain.Entities;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mycinema.Infrastructure.Repositories.Read;

public class MovieReadRepository : GenericReadRepository<Movie>, IMovieReadRepository
{
    private DbConnection _connection;

    private string _connectionString => _connection.ConnectionString;

    public MovieReadRepository(DbConnection connection) : base(connection)
    {
        _connection = connection;
    }

    public async Task<List<Movie>> GetMostSuccesfulMoviesByDate(DateTime startdatetime, DateTime endDatetime)
    {
        var sql = $"Select m.*, SUM(s.SeatsSold) sumofSeatsSold from Movie m join Session s on s.MovieId = m.Id where ReleaseDate between @StartDateTime and @EndDateTime group by m.id, m.OriginalTitle, m.ReleaseDate, m.OriginalLanguage, m.Adult order by sumofSeatsSold desc";
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryAsync<Movie>(sql, new {StartDateTime = startdatetime.ToString("yyyy-MM-dd"), EndDateTime = endDatetime.ToString("yyyy-MM-dd") });
            return result.ToList();
        }
    }
}
