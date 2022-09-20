namespace Mycinema.Application.Models.DTOs.Entities.TmdbAPI;

public class BillBoardConfigurator
{
    public BillBoard billboard { get; private set; }

    public BillBoardConfigurator(BillBoard billboard)
    {
        billboard = billboard;
    }

    public void AddLimitNumberOfMoviesRecommendations(int numberOfMovies, List<MovieRecommendation> movieRecommendations)
    {
        if (numberOfMovies > 0)
            this.billboard.Movies.AddRange(movieRecommendations.Take(numberOfMovies));
    }

    public void AddLimitNumberOfTvShowsRecommendations(int numberOfTvShows, List<TvShowRecommendation> tvShowRecommendations)
    {
        if (numberOfTvShows > 0)
            this.billboard.TvShows.AddRange(tvShowRecommendations.Take(numberOfTvShows));
    }
}
