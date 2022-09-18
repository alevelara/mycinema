using System.Text.Json.Serialization;

namespace Mycinema.Application.Models.DTOs;

public class TmdbMovieDto
{
    public bool adult { get; set; }
    public string backdrop_path { get; set; } = string.Empty;
    public int[] genre_ids { get; set; }
    public int id { get; set; }
    public string original_language { get; set; } = string.Empty;
    public string original_title { get; set; } = string.Empty;
    public string overview { get; set; } = string.Empty;
    public float popularity { get; set; }
    public string poster_path { get; set; } = string.Empty;
    public string release_date { get; set; } = string.Empty;
    public string title { get; set; } = string.Empty;
    public bool video { get; set; }
    public float vote_average { get; set; }
    public int vote_count { get; set; }

    [JsonConstructor]    
    public TmdbMovieDto(bool adult, string backdrop_path, int[] genre_ids, int id, string original_language, string original_title, string overview, float popularity, string poster_path, string release_date, string title, bool video, float vote_average, int vote_count)
    {
        this.adult = adult;
        this.backdrop_path = backdrop_path;
        this.genre_ids = genre_ids;
        this.id = id;
        this.original_language = original_language;
        this.original_title = original_title;
        this.overview = overview;
        this.popularity = popularity;
        this.poster_path = poster_path;
        this.release_date = release_date;
        this.title = title;
        this.video = video;
        this.vote_average = vote_average;
        this.vote_count = vote_count;
    }
    
    [JsonConstructor]
    public TmdbMovieDto()
    {
    }
}