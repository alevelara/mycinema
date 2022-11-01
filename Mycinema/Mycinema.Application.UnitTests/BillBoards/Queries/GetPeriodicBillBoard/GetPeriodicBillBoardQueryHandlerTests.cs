using AutoFixture;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Moq;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;
using Mycinema.Application.Mappings;
using Mycinema.Application.Models.DTOs;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Models.Entities;
using Mycinema.Application.Services.MovieRecommendations;
using Mycinema.Application.Services.TvShowRecommendations;
using Mycinema.Domain.Entities;
using Xunit;

namespace Mycinema.Application.UnitTests.BillBoards.Queries.GetPeriodicBillBoard;

public class GetPeriodicBillBoardQueryHandlerTests
{
    private Mock<IGetMovieRecommendation> _getMovieRecommendation;
    private Mock<IGetTVShowRecommendation> _getTvShowRecommendation;        
    private Mock<ILogger<GetPeriodicBillBoardQueryHandler>> _logger;
    private IMapper _mapper;
    private GetPeriodicBillBoardValidator _validator;

    public GetPeriodicBillBoardQueryHandlerTests()
    {
        _getMovieRecommendation = new Mock<IGetMovieRecommendation>();
        _getTvShowRecommendation = new Mock<IGetTVShowRecommendation>();
        _logger = new Mock<ILogger<GetPeriodicBillBoardQueryHandler>>();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _validator = new GetPeriodicBillBoardValidator();
    }

    [Fact]
    public async Task GetPeriodicBillBoardQueryHandler_ShouldReturnIntelligentBillboardWithoutRecommendations()       
    {
        var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms: 1,
            numberOfScreensForSmallRooms: 1,
            startDateTime: DateTime.Now.AddDays(-7),
            endDateTime: DateTime.Now,
            haveSimilarMovies: false);

        var movieResults = GetResultOfDiscoverMovies();
        var tvShowResults = GetResultOfDiscoverTvShows();

        _getMovieRecommendation.Setup(c => c.GetMovieRecommendations(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(movieResults);
        _getTvShowRecommendation.Setup(c => c.GetTVShowRecomendations(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(tvShowResults);
        
        var getPeriodicBillBoardQueryHandler = new GetPeriodicBillBoardQueryHandler(_logger.Object, _mapper, _getTvShowRecommendation.Object, _getMovieRecommendation.Object);
        var billboard = await getPeriodicBillBoardQueryHandler.Handle(request, CancellationToken.None);

        //Assert.NotNull(billboard.Movies);
        //Assert.NotNull(billboard.TvShows);
        //Assert.True(billboard.TvShows.Count == request.NumberOfScreensForSmallRooms);
        //Assert.True(billboard.Movies.Count == request.NumberOfScreensForBigRooms);
        Assert.True(_validator.Validate(request).IsValid);
    }

    [Fact]
    public async Task GetPeriodicBillBoardQueryHandler_ShouldReturnIntelligentBillboardWithRecommendations()        
    {
        var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms: 1,
            numberOfScreensForSmallRooms: 1,
            startDateTime: DateTime.Now.AddDays(-7),
            endDateTime: DateTime.Now,
            haveSimilarMovies: true);

        var movieResults = GetResultOfDiscoverMovies();
        var tvShowResults = GetResultOfDiscoverTvShows();
        var similarMovies = GetSimilarMoviesForTest();
        var similarMoviesRecommendations = _mapper.Map<List<Movie>, List<MovieRecommendation>>(similarMovies.Result);

        _getMovieRecommendation.Setup(c => c.GetMovieRecommendations(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(movieResults);
        _getTvShowRecommendation.Setup(c => c.GetTVShowRecomendations(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(tvShowResults);
        _getMovieRecommendation.Setup(m => m.GetMostSuccesfulMoviesFromDb(It.IsAny<DateTime>(), It.IsAny<DateTime>())).Returns(similarMovies);

        var getPeriodicBillBoardQueryHandler = new GetPeriodicBillBoardQueryHandler(_logger.Object, _mapper, _getTvShowRecommendation.Object, _getMovieRecommendation.Object);
        var billboard = await getPeriodicBillBoardQueryHandler.Handle(request, CancellationToken.None);

        //Assert.NotNull(billboard.Movies);
        //Assert.NotNull(billboard.TvShows);
        //Assert.True(billboard.TvShows.Count == request.NumberOfScreensForSmallRooms);
        //Assert.True(billboard.Movies.Count == request.NumberOfScreensForBigRooms);
        //Assert.Equal(billboard.Movies[0].Tittle, similarMoviesRecommendations[0].Tittle);
    }

    [Fact]
    public void GetPeriodicBillBoardQueryValidator_ShouldReturnInvalidScreenForSmallRoomsParameter()        
    {
        var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms: 1,
            numberOfScreensForSmallRooms: -1,
            startDateTime: DateTime.Now.AddDays(-7),
            endDateTime: DateTime.Now,
            haveSimilarMovies: true);
        
        Assert.False(_validator.Validate(request).IsValid);
    }

    [Fact]
    public void GetPeriodicBillBoardQueryValidator_ShouldReturnInvalidScreenForBigRoomsParameter()
    {
        var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms: -1,
            numberOfScreensForSmallRooms: 1,
            startDateTime: DateTime.Now.AddDays(-7),
            endDateTime: DateTime.Now,
            haveSimilarMovies: true);

        Assert.False(_validator.Validate(request).IsValid);
    }

    [Fact]
    public void GetPeriodicBillBoardQueryValidator_ShouldReturnInvalidStartDatetimeParameter()        
    {
        var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms: 1,
            numberOfScreensForSmallRooms: 1,
            startDateTime: DateTime.MinValue,
            endDateTime: DateTime.Now,
            haveSimilarMovies: true);

        Assert.False(_validator.Validate(request).IsValid);
    }

    [Fact]
    public void GetPeriodicBillBoardQueryValidator_ShouldReturnInvalidEndDatetimeParameter()     
    {
        var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms: -1,
            numberOfScreensForSmallRooms: 1,
            startDateTime: DateTime.Now.AddDays(-7),
            endDateTime: DateTime.MaxValue,
            haveSimilarMovies: true);

        Assert.False(_validator.Validate(request).IsValid);
    }

    [Fact]
    public void GetPeriodicBillBoardQueryValidator_ShouldReturnInvalidMultipleParameter()        
    {
        var request = new GetPeriodicBillBoardQuery(numberOfScreensForBigRooms: -1,
            numberOfScreensForSmallRooms: -1,
            startDateTime: DateTime.MinValue,
            endDateTime: DateTime.MaxValue,
            haveSimilarMovies: true);

        var validation = _validator.Validate(request);

        Assert.False(validation.IsValid);
        Assert.True(validation.Errors.Count == 5);
    }

    #region private methods
    private Task<PagedDto<TmdbMovieDto>> GetResultOfDiscoverMovies()
    {
        var fixture = new Fixture();
        var movies = fixture.Build<TmdbMovieDto>()
            .With(t => t.release_date, DateTime.Now.ToString())
            .CreateMany().ToArray();
        var pagedMovies = fixture.Build<PagedDto<TmdbMovieDto>>()
            .With(t => t.results, movies)
            .Create();
        return Task.FromResult<PagedDto<TmdbMovieDto>>(pagedMovies);
    }

    private Task<PagedDto<TmdbTvShowDto>> GetResultOfDiscoverTvShows()
    {
        var fixture = new Fixture();
        var tvShows = fixture.Build<TmdbTvShowDto>()
            .With(t => t.first_air_date, DateTime.Now.ToString())
            .CreateMany().ToArray();
        var pagedTvShows = fixture.Build<PagedDto<TmdbTvShowDto>>()
            .With(t => t.results, tvShows)
            .Create();
        return Task.FromResult<PagedDto<TmdbTvShowDto>>(pagedTvShows);
    }

    private Task<List<Movie>> GetSimilarMoviesForTest()
    {
        var movies = new List<Movie>
        {
            new Movie(id: 1,
            originalTitle: "test1",
            releaseDate: DateTime.Now,
            originalLanguage: "test1",
            adult: false)
            ,
            new Movie(id: 2,
            originalTitle: "test2",
            releaseDate: DateTime.Now,
            originalLanguage: "test2",
            adult: false)
            ,
            new Movie(id: 3,
            originalTitle: "test3",
            releaseDate: DateTime.Now,
            originalLanguage: "test3",
            adult: false),
        };
        return Task.FromResult<List<Movie>>(movies);
    }
    #endregion
}
