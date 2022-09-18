namespace Mycinema.Application.Models.DTOs.Entities;

public class MovieDto
{ 
    public int Id { get; set; }
    public string OriginalTitle { get; set; } 
    public DateTime ReleaseDate { get; set; } 
    public string? OriginalLanguage { get; set; }
    public bool Adult { get; set; }
}
