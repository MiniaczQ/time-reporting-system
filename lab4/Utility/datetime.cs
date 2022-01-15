using System;

namespace lab4.Utility
{
    public static class DateTimeExt
    {
        public static DateTime Dayless(this DateTime DateTime) =>
            new DateTime(DateTime.Year, DateTime.Month, 1);

        public static string DaylessString(this DateTime DateTime) =>
            DateTime.ToString("MM/yyyy");
    }
}