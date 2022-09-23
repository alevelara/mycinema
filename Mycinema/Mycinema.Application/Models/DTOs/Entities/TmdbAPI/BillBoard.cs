using Newtonsoft.Json;

namespace Mycinema.Application.Models.DTOs.Entities.TmdbAPI;

public class BillBoard
{
    public List<MovieRecommendation> Movies { get; private set; }
    public List<TvShowRecommendation> TvShows { get; private set; }

    public BillBoard(List<MovieRecommendation> movies, List<TvShowRecommendation> tvShows)
    {
        Movies = movies;
        TvShows = tvShows;
    }

    public BillBoard()
    {
        Movies = new List<MovieRecommendation>();
        TvShows = new List<TvShowRecommendation>();
    }
}
