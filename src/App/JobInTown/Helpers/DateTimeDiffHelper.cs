using System;
using Core.Extensions;
using JobInTown.Models;
using JobInTown.Models.Enums;

namespace JobInTown.Helpers
{
    public static class DateTimeDiffHelper
    {
        public static DateTimeDiff GetTimeDiffFromUtcNow(DateTime originDateTime)
        {
            return GetTimeDiff(DateTime.UtcNow, originDateTime);
        }

        public static DateTimeDiff GetTimeDiff(DateTime endDateTime, DateTime originDateTime)
        {
            DateTimeDiff dateTimeDiff = null;

            var timespan = endDateTime.Subtract(originDateTime);

            var years = timespan.GetYears();
            var months = timespan.GetMonths();

            if (years > 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = years,
                    Format = DateTimeDiffFormatType.Years
                };
            }
            else if (years == 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = years,
                    Format = DateTimeDiffFormatType.Year
                };
            }
            else if (months > 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = months,
                    Format = DateTimeDiffFormatType.Months
                };
            }
            else if (months == 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = months,
                    Format = DateTimeDiffFormatType.Month
                };
            }
            else if (timespan.Days > 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Days,
                    Format = DateTimeDiffFormatType.Days
                };
            }
            else if (timespan.Days == 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Days,
                    Format = DateTimeDiffFormatType.Day
                };
            }
            else if (timespan.Hours > 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Hours,
                    Format = DateTimeDiffFormatType.Hours
                };
            }
            else if (timespan.Hours == 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Hours,
                    Format = DateTimeDiffFormatType.Hour
                };
            }
            else if (timespan.Minutes > 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Minutes,
                    Format = DateTimeDiffFormatType.Minutes
                };
            }
            else if (timespan.Minutes == 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Minutes,
                    Format = DateTimeDiffFormatType.Minute
                };
            }
            else if (timespan.Seconds > 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Seconds,
                    Format = DateTimeDiffFormatType.Seconds
                };
            }
            else if (timespan.Seconds == 1)
            {
                dateTimeDiff = new DateTimeDiff()
                {
                    Number = timespan.Seconds,
                    Format = DateTimeDiffFormatType.Second
                };
            }

            return dateTimeDiff;
        }
    }
}
