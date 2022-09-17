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
    public class SessionReadRepositoryIntegrationTests
    {
        private readonly IDbConnection _connection;
        private readonly GenericReadRepository<Session> _sessionRepository;

        private const string testConnectionString = "Server=tcp:beezybetest.database.windows.net,1433;Initial Catalog=beezycinema;Persist Security Info=False;User ID=betestuser;Password=ReadOnly!;MultipleActiveResultSets= False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public SessionReadRepositoryIntegrationTests()
        {
            _connection = new SqlConnection(testConnectionString);
            _sessionRepository = new GenericReadRepository<Session>((DbConnection)_connection);
        }

        [Fact]
        public async Task GivenAnExistingSession_WhenCallGetByIdMethod_ThenReturnOneSession()
        {
            var session = await _sessionRepository.GetByIdAsync(1);
            Assert.NotNull(session);
        }

        [Fact]
        public async Task GivenAnInvalidSession_WhenCallGetByIdMethod_ThenReturnNull()
        {
            var session = await _sessionRepository.GetByIdAsync(-1);
            Assert.Null(session);
        }

        [Fact]
        public async Task GivenExistingSessions_WhenGetAllAsync_ThenReturnAListOfSessions()
        {
            var sessions = await _sessionRepository.GetAllAsync();
            Assert.IsType<List<Session>>(sessions);
        }

        [Fact]
        public async Task GivenExistingSessions_WhenGetAllAsync_ThenReturnFullyList()
        {
            var sessions = await _sessionRepository.GetAllAsync();
            Assert.True(sessions.Count >= 1);
        }

    }
}