using System;
using System.Collections.Generic;

namespace SirajTech.Helpers.Core
{
    public static class DateTimeExtensions
    {
        public static Dictionary<DayOfWeek, int> GetMonthesDays(this DateTime fromDate, DateTime toDate)
        {
            if (fromDate > toDate)
                throw new ArgumentOutOfRangeException("fromDate");

            var dictionary = new Dictionary<DayOfWeek, int>();
            for (var i = 1; i < 8; i++)
            {
                dictionary.Add((DayOfWeek) i, 0);
            }

            var tempDate = new DateTime(fromDate.Year, fromDate.Month, fromDate.Day);
            while (tempDate.Date <= toDate.Date)
            {
                dictionary[tempDate.DayOfWeek] = dictionary[tempDate.DayOfWeek] + 1;
                tempDate = tempDate.AddDays(1);
            }
            return dictionary;
        }



        public static DateTime GetNearest(this DateTime originaldate, int dayInWeekId, bool isBefore = true, bool isCurrentCounted = true)
        {
            return GetNearest(originaldate, (DayOfWeek) dayInWeekId, isBefore, isCurrentCounted);
        }



        public static DateTime GetNearest(this DateTime originalDate, DayOfWeek dayOfWeek, bool isBefore = true, bool isCurrentCounted = true)
        {
            var destenationDate = originalDate;
            if (isCurrentCounted)
            {
                while (destenationDate.DayOfWeek != dayOfWeek)
                {
                    var offset = isBefore
                            ? -1
                            : 1;
                    destenationDate = destenationDate.AddDays(offset);
                }
            }
            else
            {
                do
                {
                    var offset = isBefore
                            ? -1
                            : 1;
                    destenationDate = destenationDate.AddDays(offset);
                } while (destenationDate.DayOfWeek != dayOfWeek);
            }
            return destenationDate;
        }



        public static int DaysToMonthEnd(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month) - date.Day;
        }



        public static int GetMonthDays(this DateTime date)
        {
            return DateTime.DaysInMonth(date.Year, date.Month);
        }



        public static DateTimeSpan GetDateComparer(this DateTime firstDate, DateTime secondDate)
        {
            return DateTimeSpan.CompareDates(firstDate, secondDate);
        }
    }
}