using Mycinema.Domain.Entities;
using Mycinema.Infrastructure.Repositories.Read;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Xunit;

namespace MyCity.Infrastructure.IntegrationTests.Repositories
{
    public class CityReadRepositoryIntegrationTests
    {
        private readonly IDbConnection _connection;
        private readonly GenericReadRepository<City> _cityRepository;

        private const string testConnectionString = "Server=tcp:beezybetest.database.windows.net,1433;Initial Catalog=beezycinema;Persist Security Info=False;User ID=betestuser;Password=ReadOnly!;MultipleActiveResultSets= False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public CityReadRepositoryIntegrationTests()
        {
            _connection = new SqlConnection(testConnectionString);
            _cityRepository = new GenericReadRepository<City>((DbConnection)_connection);
        }

        [Fact]
        public async Task CityReadRepository_ShouldReturnSingleCity()
        {
            var city = await _cityRepository.GetByIdAsync(1);
            Assert.NotNull(city);
        }

        [Fact]
        public async Task CityReadRepository_ShouldReturnNull()
        {
            var city = await _cityRepository.GetByIdAsync(-1);
            Assert.Null(city);
        }

        [Fact]
        public async Task CityReadRepository_ShouldReturnListOfCities()
        {
            var cities = await _cityRepository.GetAllAsync();
            Assert.IsType<List<City>>(cities);
        }

        [Fact]
        public async Task CityReadRepository_ShouldReturnNotEmptyListOfCities()
        {
            var cities = await _cityRepository.GetAllAsync();
            Assert.True(cities.Count >= 1);
        }

    }
}