using Mycinema.Application.Models.Entities;
using System.Globalization;

namespace Mycinema.Application.Utils;

public static class DateUtils
{
    public static int GetNumberOfWeekFromAYear(int initialYear, DateTime date)
    {
        var yearDifference = date.Year - initialYear;
        var dinfo = DateTimeFormatInfo.CurrentInfo;
        var weeksOfInitialYear = GetTotalNumberOfWeeksInAYear(initialYear);
        return dinfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday) + (weeksOfInitialYear * yearDifference);
    }

    public static int GetNumberOfWeek(DateTime date)
    {
        var dinfo = DateTimeFormatInfo.CurrentInfo;
        return dinfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Monday);
    }

    public static Week CalculateWeekFromDate(DateTime releaseDate)
    {
        var numberOfWeek = GetNumberOfWeek(releaseDate);
        var startDateOfWeek = StartOfWeek(releaseDate);
        var endDateOfWeek = EndOfWeek(releaseDate);
        
        if (startDateOfWeek.Equals(endDateOfWeek))
            endDateOfWeek = endDateOfWeek.AddDays(7);

        return new Week(startDateOfWeek, endDateOfWeek, numberOfWeek);
    }

    public static Week CalculateFirstWeekFromDate(DateTime releaseDate)
    {
        var numberOfWeek = GetNumberOfWeek(releaseDate);        
        var endDateOfWeek = EndOfWeek(releaseDate);

        if (releaseDate.Equals(endDateOfWeek))
            endDateOfWeek = endDateOfWeek.AddDays(7);

        return new Week(releaseDate, endDateOfWeek, numberOfWeek);
    }

    public static Week CalculateLastWeekFromDate(DateTime endDateTime)
    {
        var numberOfWeek = GetNumberOfWeek(endDateTime);
        var startDateOfWeek = StartOfWeek(endDateTime);

        return new Week(startDateOfWeek, endDateTime, numberOfWeek);
    }

    private static DateTime StartOfWeek(this DateTime date)
    {
        int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(-1 * diff).Date;
    }

    private static DateTime EndOfWeek(this DateTime date)
    {
        int diff = (7 - (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(diff).Date;
    }

    public static int GetTotalNumberOfWeeksInAYear(int year)
    {
        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        DateTime date1 = new DateTime(year, 12, 31);        
        return dfi.Calendar.GetWeekOfYear(date1, CalendarWeekRule.FirstFullWeek, dfi.FirstDayOfWeek);
    }

    public static bool ValidateDate(DateTime date)
    {
        if (date == null)
            return false;

        if (date <= DateTime.MinValue)
            return false;

        if (date >= DateTime.MaxValue)
            return false;

        return true;
    }
}
