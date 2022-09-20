namespace Mycinema.Application.Models.DTOs.Entities.TmdbAPI;

public class TvShowRecommendation : Recommendation
{
    public TvShowRecommendation(string tittle, string overview, List<int> genres, string language, DateTime releaseDate, string webSite, string keywords) 
        : base(tittle, overview, genres, language, releaseDate, webSite, keywords)
    {
    }
}
