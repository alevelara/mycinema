using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Models.Entities;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;

public class GetPeriodicBillBoardQueryHandler : IRequestHandler<GetPeriodicBillBoardQuery, BillBoard>
{
    private readonly IHttpClientService _httpService;
    private readonly IMovieReadRepository _movieRepository;
    private readonly ILogger<GetPeriodicBillBoardQueryHandler> _logger;
    private readonly IMapper _mapper;

    public GetPeriodicBillBoardQueryHandler(IHttpClientService clientService, IMovieReadRepository movieRepository, ILogger<GetPeriodicBillBoardQueryHandler> logger, IMapper mapper)
    {
        _httpService = clientService;
        _movieRepository = movieRepository;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<BillBoard> Handle(GetPeriodicBillBoardQuery request, CancellationToken cancellationToken)
    {
        
        List<TvShowRecommendation> tvShowRecommendations = await GetTVShowRecomendations(request.StartDateTime, request.EndDateTime);        
        List<MovieRecommendation> moviesRecommendations = await GetMoviesRecomendations(request.StartDateTime, request.EndDateTime);

        if (request.HaveSimilarMovies)
        {
            var similarMovies = await GetMostSuccesfulMoviesFromDb(request.StartDateTime, request.EndDateTime);
            moviesRecommendations.AddRange(similarMovies);
        }
       
        var billBoardFactory = new BillBoardFactory(request.StartDateTime, request.EndDateTime, moviesRecommendations, tvShowRecommendations, request.NumberOfScreensForBigRooms, request.NumberOfScreensForSmallRooms);

        _logger.LogInformation("Billboard created succesfully");

        return billBoardFactory.CreateBillBoard();
    }

    private async Task<List<MovieRecommendation>> GetMoviesRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        List<MovieRecommendation> moviesRecommendations = new List<MovieRecommendation>();
        var tmdbMovieRecommendation = await _httpService.DiscoverMovies(startDatetime, endDatetime);
        if (tmdbMovieRecommendation != null)
            moviesRecommendations = _mapper.Map<TmdbMovieDto[], List<MovieRecommendation>>(tmdbMovieRecommendation.results);

        return moviesRecommendations;
    }

    private async Task<List<TvShowRecommendation>> GetTVShowRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        List<TvShowRecommendation> tvShowRecommendations = new List<TvShowRecommendation>();        
        var tmdbTvShowRecommendation = await _httpService.DiscoverTvShows(startDatetime, endDatetime);
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
