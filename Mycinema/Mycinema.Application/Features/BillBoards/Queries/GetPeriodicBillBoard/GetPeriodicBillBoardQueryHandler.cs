using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Models.Entities;
using Mycinema.Application.Services.MovieRecommendations;
using Mycinema.Application.Services.TvShowRecommendations;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;

public class GetPeriodicBillBoardQueryHandler : IRequestHandler<GetPeriodicBillBoardQuery, BillBoard>
{
    
    private readonly ILogger<GetPeriodicBillBoardQueryHandler> _logger;
    private readonly IMapper _mapper;
    private readonly IGetTVShowRecommendation _getTVShowRecommendation;
    private readonly IGetMovieRecommendation _getMovieRecommendation;

    public GetPeriodicBillBoardQueryHandler(ILogger<GetPeriodicBillBoardQueryHandler> logger, IMapper mapper, IGetTVShowRecommendation getTVShowRecommendation, IGetMovieRecommendation getMovieRecommendation)
    {
        _logger = logger;
        _mapper = mapper;
        _getTVShowRecommendation = getTVShowRecommendation;
        _getMovieRecommendation = getMovieRecommendation;
    }

    public async Task<BillBoard> Handle(GetPeriodicBillBoardQuery request, CancellationToken cancellationToken)
    {
        var tvShowRecommendations = await GetTVShowRecomendations(request.StartDateTime, request.EndDateTime);        
        var moviesRecommendations = await GetMoviesRecomendations(request.StartDateTime, request.EndDateTime);

        if (request.HaveSimilarMovies)
        {
            var similarMovies = await GetMostSuccesfulMoviesFromDb(request.StartDateTime, request.EndDateTime);
            moviesRecommendations.AddRange(similarMovies);
        }

        BillBoardFactory billBoardFactory = new(request, moviesRecommendations, tvShowRecommendations);

        _logger.LogInformation("Billboard created succesfully");

        return billBoardFactory.CreateBillBoard();
    }


    private async Task<List<MovieRecommendation>> GetMoviesRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        List<MovieRecommendation> moviesRecommendations = new List<MovieRecommendation>();
        var tmdbMovieRecommendation = await _getMovieRecommendation.GetMovieRecommendations(startDatetime, endDatetime);
        if (tmdbMovieRecommendation != null)
            moviesRecommendations = _mapper.Map<TmdbMovieDto[], List<MovieRecommendation>>(tmdbMovieRecommendation.results);

        return moviesRecommendations;
    }

    private async Task<List<TvShowRecommendation>> GetTVShowRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        List<TvShowRecommendation> tvShowRecommendations = new List<TvShowRecommendation>();
        var tmdbTvShowRecommendation = await _getTVShowRecommendation.GetTVShowRecomendations(startDatetime, endDatetime);
        if (tmdbTvShowRecommendation != null)
            tvShowRecommendations = _mapper.Map<TmdbTvShowDto[], List<TvShowRecommendation>>(tmdbTvShowRecommendation.results);

        return tvShowRecommendations;
    }

    private async Task<List<MovieRecommendation>> GetMostSuccesfulMoviesFromDb(DateTime startDatetime, DateTime endDatetime)
    {
        var similarMovies = await _getMovieRecommendation.GetMostSuccesfulMoviesFromDb(startDatetime, endDatetime);
        return _mapper.Map<List<Movie>, List<MovieRecommendation>>(similarMovies);
    }

}
