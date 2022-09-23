using MediatR;
using Mycinema.Application.Models.DTOs.Entities.TmdbAPI;

namespace Mycinema.Application.Features.BillBoards.Queries.GetPeriodicBillBoard;

public class GetPeriodicBillBoardQuery : IRequest<BillBoard>
{
    public int NumberOfScreensForBigRooms { get; private set; }
    public int NumberOfScreensForSmallRooms { get; private set; }
    public DateTime StartDateTime { get; private set; }
    public DateTime EndDateTime { get; private set; }
    public bool HaveSimilarMovies { get; private set; } = false;

    public GetPeriodicBillBoardQuery(int numberOfScreensForBigRooms, int numberOfScreensForSmallRooms, DateTime startDateTime, DateTime endDateTime, bool haveSimilarMovies)
    {
        NumberOfScreensForBigRooms = numberOfScreensForBigRooms;
        NumberOfScreensForSmallRooms = numberOfScreensForSmallRooms;
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        HaveSimilarMovies = haveSimilarMovies;
    }   
}
