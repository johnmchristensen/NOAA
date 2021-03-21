using System;

namespace NOAA.GHCND.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool TryAddDays(this DateTime date, int days, out DateTime result)
        {
            if ((DateTime.MaxValue - date).TotalDays < days)
            {
                result = DateTime.MaxValue;
                return false;
            }

            result = date.AddDays(days);
            return true;
        }
    }
}