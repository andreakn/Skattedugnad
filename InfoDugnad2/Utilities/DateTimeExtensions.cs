using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InfoDugnad2.Utilities
{
	public static class DateTimeExtensions
	{

		public static string Friendly(this DateTime? input)
		{
			if (input == null)
			{
				return "???";
			}
			return input.Value.Friendly();
		}
		public static string Friendly(this DateTime d)
		{
			
			var now = DateTime.Now;
			var format = "om {0}";
			var diff = d - now;
			if (now > d)
			{
				format = "for {0} siden";
				diff = now - d;
			}
			var content = Friendly(diff);
			return string.Format(format, content);
		}

		public static string Friendly(this TimeSpan diff)
		{
			string content = "lenge";
			if (Math.Round(diff.TotalSeconds) == 1)
				content = "" + Math.Round(diff.TotalSeconds) + " sek";
			if (diff.TotalSeconds < 60)
				content = "" + Math.Round(diff.TotalSeconds) + " sek";
			else if (Math.Round(diff.TotalMinutes) == 1)
				content = "" + Math.Round(diff.TotalMinutes) + " min";
			else if (diff.TotalMinutes < 60)
				content = "" + Math.Round(diff.TotalMinutes) + " min";
			else if (Math.Round(diff.TotalHours) == 1)
				content = "" + Math.Round(diff.TotalHours) + " time";
			else if (diff.TotalHours < 24)
				content = "" + Math.Round(diff.TotalHours) + " timer";
			else if (Math.Round(diff.TotalDays) == 1)
				content = "" + Math.Round(diff.TotalDays) + " dag";
			else if (diff.TotalDays < 30)
				content = "" + Math.Round(diff.TotalDays) + " dager";

			return content;
		}
	}
}