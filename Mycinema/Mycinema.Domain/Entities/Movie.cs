using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class Movie : BaseDomainModel
{
    public string OriginalTitle { get; private set; } = string.Empty;
    public DateTime ReleaseDate { get; private set; } = DateTime.MinValue;
    public string? OriginalLanguage { get; private set; } 
    public bool Adult { get; private set; } = false;
    public virtual ICollection<Genre> Genres { get; private set; }

    public Movie(int id, string originalTitle, DateTime releaseDate, string originalLanguage, bool adult) : base(id)
    {
        OriginalTitle = originalTitle;
        ReleaseDate = releaseDate;
        OriginalLanguage = originalLanguage;
        Adult = adult;
        Genres = new List<Genre>();
    }

}
