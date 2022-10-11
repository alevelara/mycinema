using Mycinema.Domain.Entities;
using Mycinema.Infrastructure.Repositories.Read;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Xunit;

namespace MyMovieGenre.Infrastructure.IntegrationTests.Repositories
{
    public class MovieGenreReadRepositoryIntegrationTests
    {
        private readonly IDbConnection _connection;
        private readonly MovieGenreReadRepository _movieGenreRepository;

        private const string testConnectionString = "Server=tcp:beezybetest.database.windows.net,1433;Initial Catalog=beezycinema;Persist Security Info=False;User ID=betestuser;Password=ReadOnly!;MultipleActiveResultSets= False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public MovieGenreReadRepositoryIntegrationTests()
        {
            _connection = new SqlConnection(testConnectionString);
            _movieGenreRepository = new MovieGenreReadRepository((DbConnection)_connection);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByMovieIdAsync_ShouldReturnListOfMovieGenre()
        {
            var movieGenres = await _movieGenreRepository.GetByMovieIdAsync(2);
            Assert.True(movieGenres.Count >= 1);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByMovieIdAsync_ShouldReturnEmptyListOfMovieGenre()
        {
            var movieGenre = await _movieGenreRepository.GetByMovieIdAsync(-1);
            Assert.True(movieGenre.Count == 0);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByGenreIdAsync_ShouldReturnListOfMovieGenre()
        {
            var movieGenres = await _movieGenreRepository.GetByGenreIdAsync(3);
            Assert.True(movieGenres.Count >= 1);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByGenreIdAsync_ShouldReturnEmptyListOfMovieGenre()
        {
            var movieGenre = await _movieGenreRepository.GetByGenreIdAsync(-1);
            Assert.True(movieGenre.Count == 0);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByGenreIdAndMovieIdAsync_ShouldReturnAGenreMovie()
        {
            var movieGenre = await _movieGenreRepository.GetByGenreIdAndMovieIdAsync(3, 1);
            Assert.NotNull(movieGenre);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByGenreIdAndMovieIdAsync_ShouldReturnNullGenreMovieWithInvalidMovieId()
        {
            var movieGenre = await _movieGenreRepository.GetByGenreIdAndMovieIdAsync(3, -1);
            Assert.Null(movieGenre);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByGenreIdAndMovieIdAsync_ShouldReturnNullGenreMovieWithInvalidGenreId()
        {
            var movieGenre = await _movieGenreRepository.GetByGenreIdAndMovieIdAsync(-1, 1);
            Assert.Null(movieGenre);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetByGenreIdAndMovieIdAsync_ShouldReturnNullGenreMovieWithInvalidParameters()
        {
            var movieGenre = await _movieGenreRepository.GetByGenreIdAndMovieIdAsync(-1, -1);
            Assert.Null(movieGenre);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetAllAsync_ShouldReturnListOfMovieGenre()
        {
            var movieGenres = await _movieGenreRepository.GetAllAsync();
            Assert.IsType<List<MovieGenre>>(movieGenres);
        }

        [Fact]
        public async Task MovieGenreReadRepository_GetAllAsync_ShouldReturnNotEmptyListOfMovieGenre()
        {
            var movieGenres = await _movieGenreRepository.GetAllAsync();
            Assert.True(movieGenres.Count >= 1);
        }

    }
}