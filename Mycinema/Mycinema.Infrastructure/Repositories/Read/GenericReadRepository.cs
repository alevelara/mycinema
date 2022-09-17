using Dapper;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Domain.Common;
using System.Data.Common;
using System.Data.SqlClient;

namespace Mycinema.Infrastructure.Repositories.Read;

public class GenericReadRepository<T> : IAsyncReadRepository<T> where T : BaseDomainModel
{
    private DbConnection _connection;

    private string _connectionString => _connection.ConnectionString;
    private string entityName => typeof(T).Name;

    public GenericReadRepository(DbConnection connection)
    {
        _connection = connection;
    }
    
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        var sql = $"SELECT * FROM {entityName}";
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QueryAsync<T>(sql);
            return result.ToList();
        }
    }
    
    public async Task<T> GetByIdAsync(int id)
    {
        var sql = $"SELECT * FROM {entityName} WHERE Id = @Id";
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var result = await connection.QuerySingleOrDefaultAsync<T>(sql, new { Id = id });
            return result;
        }
    }    
}
