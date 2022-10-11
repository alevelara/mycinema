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
    public class CinemaReadRepositoryIntegrationTests
    {
        private readonly IDbConnection _connection;
        private readonly GenericReadRepository<Cinema> _cinemaRepository;

        private const string testConnectionString = "Server=tcp:beezybetest.database.windows.net,1433;Initial Catalog=beezycinema;Persist Security Info=False;User ID=betestuser;Password=ReadOnly!;MultipleActiveResultSets= False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public CinemaReadRepositoryIntegrationTests()
        {
            _connection = new SqlConnection(testConnectionString);
            _cinemaRepository = new GenericReadRepository<Cinema>((DbConnection)_connection);
        }

        [Fact]
        public async Task CinemaReadRepository_ShouldReturnSingleCinema()
        {
            var cinema = await _cinemaRepository.GetByIdAsync(2);
            Assert.NotNull(cinema);
        }

        [Fact]
        public async Task CinemaReadRepository_ShouldReturnNull()
        {
            var cinema = await _cinemaRepository.GetByIdAsync(-1);
            Assert.Null(cinema);
        }

        [Fact]
        public async Task CinemaReadRepository_ShouldReturnListOfCinemas()
        {
            var cinemas = await _cinemaRepository.GetAllAsync();
            Assert.IsType<List<Cinema>>(cinemas);
        }

        [Fact]
        public async Task CinemaReadRepository_ShouldReturnNotEmptyListOfCinemas()
        {
            var cinemas = await _cinemaRepository.GetAllAsync();
            Assert.True(cinemas.Count >= 1);
        }

    }
}