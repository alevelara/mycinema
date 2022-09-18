namespace Mycinema.Application.Models.DTOs;

public class Recommendation
{
    public string Tittle { get; set; }
    public string Overview { get; set; }
    public List<int> Genres { get; set; }
    public string Language { get; set; }
    public DateTime ReleaseDate { get; set; }
    public string WebSite { get; set; }
    public string Keywords { get; set; }
}