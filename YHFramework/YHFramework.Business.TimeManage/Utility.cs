using System;

namespace YHFramework.Business.TimeManage
{
	public class Utility
	{
		public static void GetWeek(DateTime date, out int year, out int month, out int week)
		{
			year = 0;
			month = 0;
			week = 0;
			int beginWeek = 1;
			DateTime monthFirstDay = new DateTime(date.Year, date.Month, 1);
			if (monthFirstDay.DayOfWeek.GetHashCode() <= 4 && monthFirstDay.DayOfWeek.GetHashCode() != 0)
			{
				beginWeek = 2;
			}
			DateTime firstWeekFirstDay = monthFirstDay.AddDays((double)(7 - ((monthFirstDay.DayOfWeek.GetHashCode() == 0) ? 7 : monthFirstDay.DayOfWeek.GetHashCode()) + 1));
			DateTime monthLastDay = monthFirstDay.AddMonths(1).AddDays(-1.0);
			DateTime lastWeekFirstDay;
			if (monthLastDay.DayOfWeek.GetHashCode() % 7 == 0)
			{
				lastWeekFirstDay = monthLastDay.AddDays(-6.0);
			}
			else
			{
				lastWeekFirstDay = monthLastDay.AddDays((double)(1 - monthLastDay.DayOfWeek.GetHashCode()));
			}
			if (date.CompareTo(lastWeekFirstDay) >= 0)
			{
				if (monthLastDay.DayOfWeek.GetHashCode() < 4 && monthLastDay.DayOfWeek.GetHashCode() != 0)
				{
					if (12 == month)
					{
						year = date.Year + 1;
						month = 1;
					}
					else
					{
						year = date.Year;
						month = date.Month + 1;
					}
					week = 1;
				}
				else
				{
					year = date.Year;
					month = date.Month;
					week = lastWeekFirstDay.AddDays((double)(1 - lastWeekFirstDay.DayOfWeek.GetHashCode())).Day / 7 + beginWeek;
				}
			}
			else if (date.CompareTo(firstWeekFirstDay) < 0)
			{
				if (monthFirstDay.DayOfWeek.GetHashCode() <= 4 && monthFirstDay.DayOfWeek.GetHashCode() != 0)
				{
					year = date.Year;
					month = date.Month;
					week = 1;
				}
				else
				{
					if (1 == date.Month)
					{
						year = date.Year - 1;
						month = 12;
					}
					else
					{
						year = date.Year;
						month = date.Month - 1;
					}
					DateTime lastMonthLastDay = monthLastDay.AddMonths(-1);
					int temYear = 0;
					Utility.GetWeek(lastMonthLastDay, out temYear, out month, out week);
				}
			}
			else
			{
				year = date.Year;
				month = date.Month;
				week = (date.AddDays((double)(1 - ((date.DayOfWeek.GetHashCode() == 0) ? 7 : date.DayOfWeek.GetHashCode()))).Day - firstWeekFirstDay.Day) / 7 + beginWeek;
			}
		}
	}
}
