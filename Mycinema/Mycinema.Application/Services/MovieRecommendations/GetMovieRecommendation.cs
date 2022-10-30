using FluentValidation.Results;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Contracts.Repositories;
using Mycinema.Application.Exceptions;
using Mycinema.Application.Exceptions.ValidationErrors.MoviesRecommendations;
using Mycinema.Application.Models.DTOs;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Utils;
using Mycinema.Domain.Entities;

namespace Mycinema.Application.Services.MovieRecommendations;

public class GetMovieRecommendation : IGetMovieRecommendation
{
    private readonly IHttpClientService _httpService;
    private readonly IMovieReadRepository _movieRepository;

    public GetMovieRecommendation(IHttpClientService httpService, IMovieReadRepository movieRepository)
    {
        _httpService = httpService;
        _movieRepository = movieRepository;
    }

    public async Task<List<Movie>> GetMostSuccesfulMoviesFromDb(DateTime startDatetime, DateTime endDatetime)
    {
        if (!DateUtils.ValidateDate(startDatetime) || !DateUtils.ValidateDate(endDatetime))
        {
            throw new ValidationException(
                new List<ValidationFailure>()
                {
                    MovieRecommendationError.INVALID_PARAMETER
                });
        }

        return await _movieRepository.GetMostSuccesfulMoviesByDate(startDatetime, endDatetime);
    }

    public async Task<PagedDto<TmdbMovieDto>> GetMovieRecommendations(DateTime startDatetime, DateTime endDatetime)
    {        
        if(!DateUtils.ValidateDate(startDatetime) || !DateUtils.ValidateDate(endDatetime))
        {
            throw new ValidationException(
                new List<ValidationFailure>()
                {
                    MovieRecommendationError.INVALID_PARAMETER
                });
        }

        return await _httpService.DiscoverMovies(startDatetime, endDatetime);
    }    
}
