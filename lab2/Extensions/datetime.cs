using System;

namespace lab1.Datetime
{
    public static class DateTimeExt
    {
        public static DateTime Dayless(this DateTime DateTime) =>
            new DateTime(DateTime.Year, DateTime.Month, 1);
    }
}