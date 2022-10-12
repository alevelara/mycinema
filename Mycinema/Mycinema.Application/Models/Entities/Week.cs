namespace Mycinema.Application.Models.Entities;

public class Week
{
    public DateTime StartDateTime { get; set; }
    public DateTime EndDateTime { get; set; }
    public int NumberOfWeek { get; set; }

    public Week(DateTime startDateTime, DateTime endDateTime, int numberOfWeek)
    {
        StartDateTime = startDateTime;
        EndDateTime = endDateTime;
        NumberOfWeek = numberOfWeek;
    }

    public Week()
    {
    }
}
