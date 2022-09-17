﻿using Mycinema.Application.Contracts.Repositories;
using Mycinema.Domain.Common;
using Mycinema.Domain.Entities;
using Mycinema.Infrastructure.Repositories.Read;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Xunit;

namespace Mycinema.Infrastructure.IntegrationTests.Repositories
{
    public class GenreReadRepositoryIntegrationTests
    {
        private readonly IDbConnection _connection;
        private readonly GenericReadRepository<Genre> _genreRepository;

        private const string testConnectionString = "Server=tcp:beezybetest.database.windows.net,1433;Initial Catalog=beezycinema;Persist Security Info=False;User ID=betestuser;Password=ReadOnly!;MultipleActiveResultSets= False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public GenreReadRepositoryIntegrationTests()
        {
            _connection = new SqlConnection(testConnectionString);
            _genreRepository = new GenericReadRepository<Genre>((DbConnection)_connection);
        }

        [Fact]
        public async Task GivenAnExistingGenre_WhenCallGetByIdMethod_ThenReturnOneGenre()
        {
            var genre = await _genreRepository.GetByIdAsync(1);
            Assert.NotNull(genre);
        }

        [Fact]
        public async Task GivenAnInvalidGenre_WhenCallGetByIdMethod_ThenReturnNull()
        {
            var genre = await _genreRepository.GetByIdAsync(-1);
            Assert.Null(genre);
        }

        [Fact]
        public async Task GivenExistingGenres_WhenGetAllAsync_ThenReturnAListOfGenres()
        {
            var genres = await _genreRepository.GetAllAsync();
            Assert.IsType<List<Genre>>(genres);
        }

        [Fact]
        public async Task GivenExistingGenres_WhenGetAllAsync_ThenReturnFullyList()
        {
            var genres = await _genreRepository.GetAllAsync();
            Assert.True(genres.Count >= 1);
        }

    }
}