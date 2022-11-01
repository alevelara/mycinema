using FluentValidation.Results;
using Mycinema.Application.Contracts.Infrastructure;
using Mycinema.Application.Exceptions;
using Mycinema.Application.Exceptions.ValidationErrors.MoviesRecommendations;
using Mycinema.Application.Models.DTOs;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Utils;

namespace Mycinema.Application.Services.TvShowRecommendations;

public class GetTVShowRecommendation : IGetTVShowRecommendation
{
    private readonly IHttpClientService _httpService;

    public GetTVShowRecommendation(IHttpClientService httpService)
    {
        _httpService = httpService;
    }

    public async Task<PagedDto<TmdbTvShowDto>> GetTVShowRecomendations(DateTime startDatetime, DateTime endDatetime)
    {
        if (!DateUtils.ValidateDate(startDatetime) || !DateUtils.ValidateDate(endDatetime))
        {
            throw new ValidationException(
                new List<ValidationFailure>()
                {
                    MovieRecommendationError.INVALID_PARAMETER
                });
        }
        return await _httpService.DiscoverTvShows(startDatetime, endDatetime);
    }
}
