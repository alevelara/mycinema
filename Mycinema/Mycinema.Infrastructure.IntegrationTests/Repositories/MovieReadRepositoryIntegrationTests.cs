using Mycinema.Application.Contracts.Repositories;
using Mycinema.Domain.Common;
using Mycinema.Domain.Entities;
using Mycinema.Infrastructure.Repositories.Read;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Xunit;

namespace Mycinema.Infrastructure.IntegrationTests.Repositories
{
    public class MovieReadRepositoryIntegrationTests
    {
        private readonly IDbConnection _connection;
        private readonly GenericReadRepository<Movie> _movieRepository;

        private const string testConnectionString = "Server=tcp:beezybetest.database.windows.net,1433;Initial Catalog=beezycinema;Persist Security Info=False;User ID=betestuser;Password=ReadOnly!;MultipleActiveResultSets= False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public MovieReadRepositoryIntegrationTests()
        {
            _connection = new SqlConnection(testConnectionString);
            _movieRepository = new GenericReadRepository<Movie>((DbConnection)_connection);
        }

        [Fact]
        public async Task MovieReadRepository_ShouldReturnSingleMovie()
        {
            var movie = await _movieRepository.GetByIdAsync(1);
            Assert.NotNull(movie);
        }

        [Fact]
        public async Task MovieReadRepository_ShouldReturnNull()
        {
            var movie = await _movieRepository.GetByIdAsync(-1);
            Assert.Null(movie);
        }

        [Fact]
        public async Task MovieReadRepository_ShouldReturnListOfMovies()
        {
            var movies = await _movieRepository.GetAllAsync();
            Assert.IsType<List<Movie>>(movies);
        }

        [Fact]
        public async Task MovieReadRepository_ShouldReturnNotEmptyListOfMovies()
        {
            var movies = await _movieRepository.GetAllAsync();
            Assert.True(movies.Count >= 1);
        }

    }
}