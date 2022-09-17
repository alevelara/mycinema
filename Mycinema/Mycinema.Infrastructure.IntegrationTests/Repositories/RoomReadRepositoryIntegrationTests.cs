using Mycinema.Domain.Entities;
using Mycinema.Infrastructure.Repositories.Read;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Xunit;

namespace Mycinema.Infrastructure.IntegrationTests.Repositories
{
    public class RoomReadRepositoryIntegrationTests
    {
        private readonly IDbConnection _connection;
        private readonly GenericReadRepository<Room> _roomRepository;

        private const string testConnectionString = "Server=tcp:beezybetest.database.windows.net,1433;Initial Catalog=beezycinema;Persist Security Info=False;User ID=betestuser;Password=ReadOnly!;MultipleActiveResultSets= False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public RoomReadRepositoryIntegrationTests()
        {
            _connection = new SqlConnection(testConnectionString);
            _roomRepository = new GenericReadRepository<Room>((DbConnection)_connection);
        }

        [Fact]
        public async Task GivenAnExistingRoom_WhenCallGetByIdMethod_ThenReturnOneRoom()
        {
            var room = await _roomRepository.GetByIdAsync(25);
            Assert.NotNull(room);
        }

        [Fact]
        public async Task GivenAnInvalidRoom_WhenCallGetByIdMethod_ThenReturnNull()
        {
            var room = await _roomRepository.GetByIdAsync(-1);
            Assert.Null(room);
        }

        [Fact]
        public async Task GivenExistingRooms_WhenGetAllAsync_ThenReturnAListOfRooms()
        {
            var rooms = await _roomRepository.GetAllAsync();
            Assert.IsType<List<Room>>(rooms);
        }

        [Fact]
        public async Task GivenExistingRooms_WhenGetAllAsync_ThenReturnFullyList()
        {
            var rooms = await _roomRepository.GetAllAsync();
            Assert.True(rooms.Count >= 1);
        }

    }
}