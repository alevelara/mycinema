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
}
