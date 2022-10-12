using Mycinema.Application.Utils;
using System.Globalization;

namespace Mycinema.Application.Models.Entities;

public class BillBoardFactory
{
	private DateTime StartDateTime { get; set; }
	private DateTime EndDateTime { get; set; }

	private List<MovieRecommendation> movies { get; set; }
	private List<TvShowRecommendation> tvShows { get; set; }
	private int NumberOfWeeks { get; set; }

	public BillBoardFactory(DateTime startDateTime, DateTime endDateTime, List<MovieRecommendation> movies, List<TvShowRecommendation> tvShows)
	{
		StartDateTime = startDateTime;
		EndDateTime = endDateTime;
		this.movies = movies;
		this.tvShows = tvShows;
		NumberOfWeeks = GetNumberOfWeeks();
	}

	/*public BillBoard CreateBillBoard()
	{

	}

	private BillBoardWeeks CreateBillBoardWeeks()
	{
		BillBoardWeeks weeks = new();


	}*/


	private int GetNumberOfWeeks()
	{
		var startWeek = DateUtils.GetNumberOfWeekFromAYear(StartDateTime.Year, StartDateTime);
		var endWeek = DateUtils.GetNumberOfWeekFromAYear(StartDateTime.Year, EndDateTime);

		return endWeek - startWeek;
	}

	
}
