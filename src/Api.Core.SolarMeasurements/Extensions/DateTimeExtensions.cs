using System;

namespace Api.Core.SolarMeasurements.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime CheckOlderThanOrThrow(this DateTime timestamp, DateTime newerTimestamp)
        {
            if (timestamp > newerTimestamp)
            {
                throw new ArgumentException($"Timestamp {timestamp} must be older than {newerTimestamp}");
            }
            return timestamp;
        }
        public static DateTime CheckYearGreaterThanOrThrow(this DateTime timestamp, int initialYear)
        {
            if (timestamp.Year < initialYear)
            {
                throw new ArgumentOutOfRangeException($"The year of timestamp {timestamp} cannot be earlier than {initialYear}");
            }
            return timestamp;
        }

        public static DateTime CheckDateInThePastOrThrow(this DateTime timestamp)
        {
            if (timestamp > DateTime.Now)
            {
                throw new ArgumentOutOfRangeException($"Timestamp {timestamp} cannot be in the future");
            }

            return timestamp;
        }
    }
}