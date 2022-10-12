namespace Mycinema.Application.Models.DTOs;

public class SessionDto
{
    public Guid Id { get; set; }
    public int RoomId { get; set; }
    public int MovieId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public int? SeatsSold { get; set; }
}
