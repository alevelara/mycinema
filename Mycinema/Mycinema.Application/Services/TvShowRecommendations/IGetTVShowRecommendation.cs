using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;
using Mycinema.Application.Models.DTOs;
using Mycinema.Application.Models.Entities;

namespace Mycinema.Application.Services.TvShowRecommendations;

public interface IGetTVShowRecommendation
{
    public Task<PagedDto<TmdbTvShowDto>> GetTVShowRecomendations(DateTime startDatetime, DateTime endDatetime);
}
