using Mycinema.Application.Utils;
using System.Globalization;

namespace Mycinema.Application.Models.Entities;

public class BillBoardFactory
{
	private DateTime StartDateTime { get; set; }
	private DateTime EndDateTime { get; set; }
	private List<MovieRecommendation> Movies { get; set; }
	private List<TvShowRecommendation> TvShows { get; set; }
	private int NumberOfScreensMovies { get; set; }
    private int NumberOfScreensTvShows { get; set; }
    private int NumberOfWeeks { get; set; }

	public BillBoardFactory(DateTime startDateTime, DateTime endDateTime, List<MovieRecommendation> movies, List<TvShowRecommendation> tvShows, int numberOfScreensMovies, int numberOfScreensTvShows)
	{
		StartDateTime = startDateTime;
		EndDateTime = endDateTime;
		Movies = movies;
		TvShows = tvShows;
		NumberOfWeeks = GetNumberOfWeeks();
		NumberOfScreensMovies = numberOfScreensMovies;
		NumberOfScreensTvShows = numberOfScreensTvShows;
    }

	public BillBoard CreateBillBoard()
	{
		BillBoard billboard = new();
		for(int i = 0; i < NumberOfWeeks; i++)
		{
			var week = DateUtils.CalculateWeekFromDate(StartDateTime);
			billboard.Weeks.Add(CreateBillBoardWeeks(week));
			StartDateTime = StartDateTime.AddDays(7);
		}

		return billboard;
	}

	private BillBoardWeeks CreateBillBoardWeeks(Week week)
	{
        BillBoardWeeks weeks = new();        
		weeks.Movies.AddRange(GetMoviesForWeeks(week));
        weeks.TvShows.AddRange(GetTvShowsForWeek(week));			

		return weeks;
	}

	private int GetNumberOfWeeks()
	{
		var startWeek = DateUtils.GetNumberOfWeekFromAYear(StartDateTime.Year, StartDateTime);
		var endWeek = DateUtils.GetNumberOfWeekFromAYear(StartDateTime.Year, EndDateTime);

		return endWeek - startWeek;
	}

	private List<MovieRecommendation> GetMoviesForWeeks(Week week)
	{
		return Movies.FindAll(c => week.Equals(c.ReleaseWeek)).Take(NumberOfScreensMovies).ToList(); 
	}

    private List<TvShowRecommendation> GetTvShowsForWeek(Week week)
    {
        return TvShows.FindAll(c => week.Equals(c.ReleaseWeek)).Take(NumberOfScreensTvShows).ToList();
    }
}
