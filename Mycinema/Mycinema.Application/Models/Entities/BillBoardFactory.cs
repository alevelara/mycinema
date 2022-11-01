using Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;
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

	public BillBoardFactory(GetPeriodicBillBoardQuery request, List<MovieRecommendation> movies, List<TvShowRecommendation> tvShows)
	{
		StartDateTime = request.StartDateTime;
		EndDateTime = request.EndDateTime;
		Movies = movies;
		TvShows = tvShows;
		NumberOfWeeks = GetNumberOfWeeks();
		NumberOfScreensMovies = request.NumberOfScreensForBigRooms;
		NumberOfScreensTvShows = request.NumberOfScreensForSmallRooms;
    }

	public BillBoard CreateBillBoard()
	{
		BillBoard billboard = new();
		for(int i = 0; i < NumberOfWeeks; i++)
		{
			var week = new Week();
			if (i == 0)
			{
                week = DateUtils.CalculateFirstWeekFromDate(StartDateTime);
                billboard.Weeks.Add(CreateBillBoardWeeks(week));
                StartDateTime = StartDateTime.AddDays(7);
                continue;
            }            

            if (i == NumberOfWeeks - 1)
			{
                week = DateUtils.CalculateLastWeekFromDate(EndDateTime);
                billboard.Weeks.Add(CreateBillBoardWeeks(week));
				break;
            }

            week = DateUtils.CalculateWeekFromDate(StartDateTime);
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
		weeks.SetStartDateTime(week.StartDateTime);
        weeks.SetEndDateTime(week.EndDateTime);

        return weeks;
	}

	private int GetNumberOfWeeks()
	{
		var startWeek = DateUtils.GetNumberOfWeekFromAYear(StartDateTime.Year, StartDateTime);
		var endWeek = DateUtils.GetNumberOfWeekFromAYear(StartDateTime.Year, EndDateTime);

		return endWeek - startWeek + 1;
	}

	private List<MovieRecommendation> GetMoviesForWeeks(Week week)
	{
		return Movies.FindAll(c => c.ReleaseDate > week.StartDateTime && c.ReleaseDate <= week.EndDateTime).Take(NumberOfScreensMovies).ToList(); 
	}

    private List<TvShowRecommendation> GetTvShowsForWeek(Week week)
    {
        return TvShows.FindAll(c => c.ReleaseDate > week.StartDateTime && c.ReleaseDate <= week.EndDateTime).Take(NumberOfScreensTvShows).ToList();
    }
}
