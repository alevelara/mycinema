using Newtonsoft.Json;

namespace Mycinema.Application.Models.DTOs.Entities.TmdbAPI;

public class Recommendation
{
    public string Tittle { get; private set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Overview { get; private set; }
    public List<int> Genres { get; private set; }
    public string Language { get; private set; }
    public DateTime ReleaseDate { get; private set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string WebSite { get; private set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Keywords { get; private set; }

    public Recommendation(string tittle, string overview, List<int> genres, string language, DateTime releaseDate, string webSite, string keywords)
    {
        Tittle = tittle;
        Overview = overview;
        Genres = genres;
        Language = language;
        ReleaseDate = releaseDate;
        WebSite = webSite;
        Keywords = keywords;
    }

    public Recommendation()
    {
    }
}