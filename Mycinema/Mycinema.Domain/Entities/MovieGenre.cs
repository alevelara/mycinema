namespace Mycinema.Domain.Entities;

public class MovieGenre
{
    public int MovieId { get; private set; }
    public int GenreId { get; private set; }

    public MovieGenre(int movieId, int genreId)
    {
        MovieId = movieId;
        GenreId = genreId;
    }
}
