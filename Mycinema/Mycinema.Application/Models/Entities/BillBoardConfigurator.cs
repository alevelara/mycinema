namespace Mycinema.Application.Models.Entities;

public class BillBoardConfigurator
{
    public BillBoard Billboard { get; private set; }

    public BillBoardConfigurator(BillBoard billboard)
    {
        Billboard = billboard;
    }

    public void AddLimitNumberOfMoviesRecommendations(int numberOfMovies, List<MovieRecommendation> movieRecommendations)
    {
        if (movieRecommendations.Count > 0)
            Billboard.Movies.AddRange(movieRecommendations.Take(numberOfMovies));
    }

    public void AddLimitNumberOfTvShowsRecommendations(int numberOfTvShows, List<TvShowRecommendation> tvShowRecommendations)
    {
        if (tvShowRecommendations.Count > 0)
            Billboard.TvShows.AddRange(tvShowRecommendations.Take(numberOfTvShows));
    }
}
