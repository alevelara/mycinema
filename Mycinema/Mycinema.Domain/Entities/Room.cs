using Mycinema.Domain.Common;

namespace Mycinema.Domain.Entities;

public class Room : BaseDomainModel
{
    public string Name { get; private set; } = string.Empty;
    public string Size { get; private set; } = string.Empty;
    public int Seats { get; private set; } = 0;
    public int CinemaId { get; private set; }

    public virtual Cinema Cinema { get; set; }

    public Room(int id, string name, string size, int seats, int cinemaId) : base(id)
    {
        Name = name;
        Size = size;
        Seats = seats;
        CinemaId = cinemaId;
    }
}
