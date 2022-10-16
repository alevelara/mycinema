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
    
    public override bool Equals(Object obj)
    {
        if ((obj == null) || !this.GetType().Equals(obj.GetType()))
        {
            return false;
        }
        Week week = (Week)obj;
        return StartDateTime.Equals(week.StartDateTime) && EndDateTime.Equals(week.EndDateTime) && NumberOfWeek.Equals(week.NumberOfWeek);
    }

}
