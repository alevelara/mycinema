using AutoMapper;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Services.MovieRecommendations;
using Mycinema.Application.Services.TvShowRecommendations;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Models.Entities;

public class BillBoardConfiguration
{
    
    private readonly IMapper _mapper;
    private readonly IGetTVShowRecommendation getTVShowRecommendation;
    private readonly IGetMovieRecommendation getMovieRecommendation;

    public BillBoardConfiguration(IMapper mapper, IGetTVShowRecommendation getTVShowRecommendation, IGetMovieRecommendation getMovieRecommendation)
    {
        _mapper = mapper;
        this.getTVShowRecommendation = getTVShowRecommendation;
        this.getMovieRecommendation = getMovieRecommendation;
    }

    public async Task<List<MovieRecommendation>> GetMoviesRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        var tmdbMovieRecommendation = await getMovieRecommendation.GetMovieRecommendations(startDatetime, endDatetime);
        if (tmdbMovieRecommendation != null)
            moviesRecommendations = _mapper.Map<TmdbMovieDto[], List<MovieRecommendation>>(tmdbMovieRecommendation.results);

        return moviesRecommendations;
    }

    public async Task<List<TvShowRecommendation>> GetTVShowRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        List<TvShowRecommendation> tvShowRecommendations = new List<TvShowRecommendation>();
        var tmdbTvShowRecommendation = await _httpService.DiscoverTvShows(startDatetime, endDatetime);
        if (tmdbTvShowRecommendation != null)
            tvShowRecommendations = _mapper.Map<TmdbTvShowDto[], List<TvShowRecommendation>>(tmdbTvShowRecommendation.results);

        return tvShowRecommendations;
    }

    public async Task<List<MovieRecommendation>> GetMostSuccesfulMoviesFromDb(DateTime startDatetime, DateTime endDatetime)
    {
        var similarMovies = await _movieRepository.GetMostSuccesfulMoviesByDate(startDatetime, endDatetime);
        return _mapper.Map<List<Movie>, List<MovieRecommendation>>(similarMovies);
    }

}
