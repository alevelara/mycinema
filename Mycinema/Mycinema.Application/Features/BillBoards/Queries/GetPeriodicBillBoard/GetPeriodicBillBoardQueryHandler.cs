using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;

public class GetPeriodicBillBoardQueryHandler : IRequestHandler<GetPeriodicBillBoardQuery, BillBoard>
{
    private readonly IHttpClientService _clientService;
    private readonly IMovieReadRepository _movieRepository;
    private readonly ILogger<GetPeriodicBillBoardQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetPeriodicBillBoardQueryHandler(IHttpClientService clientService, IMovieReadRepository movieRepository, ILogger<GetPeriodicBillBoardQueryHandler> logger, IMapper mapper)
    {
        _clientService = clientService;
        _movieRepository = movieRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BillBoard> Handle(GetPeriodicBillBoardQuery request, CancellationToken cancellationToken)
    {
        var billBoard = new BillBoard();
        var billboardConfigurator = new BillBoardConfigurator(billBoard);        

        List<TvShowRecommendation> tvShowRecommendations = await GetTVShowRecomendations(request.StartDateTime, request.EndDateTime);
        billboardConfigurator.AddLimitNumberOfTvShowsRecommendations(request.NumberOfScreensForSmallRooms, tvShowRecommendations);

        List<MovieRecommendation> moviesRecommendations = await GetMoviesRecomendations(request.StartDateTime, request.EndDateTime);

        if (!request.HaveSimilarMovies)
        {
            billboardConfigurator.AddLimitNumberOfMoviesRecommendations(request.NumberOfScreensForBigRooms, moviesRecommendations);
            return billBoard;
        }

        var similarMovies = await GetMostSuccesfulMoviesFromDb(request.StartDateTime, request.EndDateTime);
        similarMovies.AddRange(moviesRecommendations);
        billboardConfigurator.AddLimitNumberOfMoviesRecommendations(request.NumberOfScreensForBigRooms, similarMovies);

        _logger.LogInformation("Billboard created succesfully");

        return billBoard;
    }

    private async Task<List<MovieRecommendation>> GetMoviesRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        List<MovieRecommendation> moviesRecommendations = new List<MovieRecommendation>();
        var tmdbMovieRecommendation = await _clientService.DiscoverMovies(startDatetime, endDatetime);
        if (tmdbMovieRecommendation != null)
            moviesRecommendations = _mapper.Map<TmdbMovieDto[], List<MovieRecommendation>>(tmdbMovieRecommendation.results);

        return moviesRecommendations;
    }

    private async Task<List<TvShowRecommendation>> GetTVShowRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        List<TvShowRecommendation> tvShowRecommendations = new List<TvShowRecommendation>();        
        var tmdbTvShowRecommendation = await _clientService.DiscoverTvShows(startDatetime, endDatetime);
        if (tmdbTvShowRecommendation != null)
            tvShowRecommendations = _mapper.Map<TmdbTvShowDto[], List<TvShowRecommendation>>(tmdbTvShowRecommendation.results);

        return tvShowRecommendations;
    }

    private async Task<List<MovieRecommendation>> GetMostSuccesfulMoviesFromDb(DateTime startDatetime, DateTime endDatetime)
    {
        var similarMovies = await _movieRepository.GetMostSuccesfulMoviesByDate(startDatetime, endDatetime);
        return  _mapper.Map<List<Movie>, List<MovieRecommendation>>(similarMovies);
    }
    
}
