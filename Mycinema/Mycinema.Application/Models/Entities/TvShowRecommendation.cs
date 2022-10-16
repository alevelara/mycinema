using Mycinema.Application.Utils;
using Newtonsoft.Json;

namespace Mycinema.Application.Models.Entities;

public class TvShowRecommendation : Recommendation
{
    public TvShowRecommendation(string tittle, string overview, List<int> genres, string language, DateTime releaseDate, string webSite, string keywords)
        : base(tittle, overview, genres, language, releaseDate, webSite, keywords)
    {
        ReleaseWeek = DateUtils.CalculateWeekFromDate(releaseDate);
    }

    public TvShowRecommendation() : base()
    {

    }
}
