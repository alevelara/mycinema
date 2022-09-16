using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class Session : BaseDomainModel
{
    public int RoomId { get; private set; }
    public int MovieId { get; private set; }
    public DateTime StartTime{ get; private set; }
    public DateTime EndTime { get; private set; }
    public int? SeatsSold { get; private set; }

    public virtual Room Room { get; private set; }
    public virtual Movie Movie{ get; private set; }

    public Session(int roomId, int movieId, DateTime startTime, DateTime endTime, int? seatsSold)
    {
        RoomId = roomId;
        MovieId = movieId;
        StartTime = startTime;
        EndTime = endTime;
        SeatsSold = seatsSold;
    }
}
