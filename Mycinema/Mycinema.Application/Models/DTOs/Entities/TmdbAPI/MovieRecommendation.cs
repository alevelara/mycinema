namespace Mycinema.Application.Models.DTOs.Entities.TmdbAPI;

public class MovieRecommendation : Recommendation
{
    public MovieRecommendation(string tittle, string overview, List<int> genres, string language, DateTime releaseDate, string webSite, string keywords)
            : base(tittle, overview, genres, language, releaseDate, webSite, keywords) { }

    public MovieRecommendation() : base()
    {

    }
}
