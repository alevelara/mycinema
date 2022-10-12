using Mycinema.Application.Models.Entities;
using System.Globalization;

namespace Mycinema.Application.Utils;

public static class DateUtils
{
    public static int GetNumberOfWeekFromAYear(int initialYear, DateTime date)
    {
        var yearDifference = date.Year - initialYear;
        var dinfo = DateTimeFormatInfo.CurrentInfo;
        var weeksOfInitialYear = GetTotalNumberOfAYear(initialYear);
        return dinfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday) + (weeksOfInitialYear * yearDifference);
    }

    public static int GetNumberOfWeek(DateTime date)
    {
        var dinfo = DateTimeFormatInfo.CurrentInfo;
        return dinfo.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstFullWeek, DayOfWeek.Monday);
    }

    public static Week CalculateWeekFromDate(DateTime releaseDate)
    {
        var numberOfWeek = GetNumberOfWeek(releaseDate);
        var startDateOfWeek = StartOfWeek(releaseDate);
        var endDateOfWeek = EndOfWeek(releaseDate);

        return new Week(startDateOfWeek, endDateOfWeek, numberOfWeek);
    }

    public static DateTime StartOfWeek(this DateTime date)
    {
        int diff = (7 + (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(-1 * diff).Date;
    }

    public static DateTime EndOfWeek(this DateTime date)
    {
        int diff = (7 - (date.DayOfWeek - DayOfWeek.Monday)) % 7;
        return date.AddDays(diff).Date;
    }

    public static int GetTotalNumberOfAYear(int year)
    {
        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
        DateTime date1 = new DateTime(year, 12, 31);        
        return dfi.Calendar.GetWeekOfYear(date1, CalendarWeekRule.FirstFullWeek, dfi.FirstDayOfWeek);
    }
}
