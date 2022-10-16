namespace Mycinema.Application.Models.Entities;

public class BillBoardWeeks
{
    public List<MovieRecommendation> Movies { get; private set; }
    public List<TvShowRecommendation> TvShows { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }

    public BillBoardWeeks(List<MovieRecommendation> movies, List<TvShowRecommendation> tvShows, DateTime startDateTime)
    {
        Movies = movies;
        TvShows = tvShows;
        StartDateTime = startDateTime;
        EndDateTime = startDateTime.AddDays(7);
    }

    public BillBoardWeeks()
    {
        Movies = new List<MovieRecommendation>();
        TvShows = new List<TvShowRecommendation>();
        StartDateTime = DateTime.Now;
        EndDateTime = DateTime.Now.AddDays(7);
    }

    public void SetStartDateTime(DateTime dateTime)
    {
        StartDateTime = dateTime;
    }

    public void SetEndDateTime(DateTime dateTime)
    {
        EndDateTime = dateTime;
    }

}
