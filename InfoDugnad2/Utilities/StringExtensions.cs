using System;
using System.Linq;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Skattedugnad.Utilities
{
	public static class StringExtensions
	{
        public static string ReplaceFirstMatch(this string value, string pattern, string newValue)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var regex = new Regex(pattern);
            if (!regex.IsMatch(value))
            {
                return value;
            }

            var match = regex.Match(value);
            var oldValue = match.Groups[0].Value;
            return string.IsNullOrEmpty(oldValue)
                ? value
                : value.Replace(match.Groups[0].Value, newValue);
        }

        public static string RemoveFirstMatch(this string value, string pattern)
        {
            return value.ReplaceFirstMatch(pattern, string.Empty);
        }

		public static string UrlSafe(this string input)
		{
		    if (string.IsNullOrEmpty(input))
		    {
		        return string.Empty;
		    }
			return input.ToLowerInvariant()
				.Replace(" ", "")
				.Replace("æ", "a")
				.Replace("ø", "o")
				.Replace("å", "a");
		}

	    public static string[] SplitByCommaNullSafe(this string value, bool removeEmptyEntries = true)
	    {
	        var splitOption = removeEmptyEntries ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None;
	        return string.IsNullOrWhiteSpace(value)
	            ? new string[0]
	            : value.Split(new[] {','}, splitOption);
	    }

	    public static int[] SplitToNumbersByComma(this string value)
	    {
	        return string.IsNullOrWhiteSpace(value)
	            ? new int[0]
	            : value.Split(',').Where(v => !string.IsNullOrWhiteSpace(v)).Select(v => int.Parse(v.Trim())).ToArray();
	    }

        public static string RemoveInvalidCharacters(this string text)
        {
            return text == null ? null : Regex.Replace(text, "[^A-Za-z0-9+,.@/sæøåÆØÅ _-]+", "");
        }

	    public static bool EqualsIgnoreCase(this string value, string otherValue)
	    {
	        return value == null
	            ? otherValue == null
	            : value.Equals(otherValue, StringComparison.InvariantCultureIgnoreCase);
	    }

	    public static T FromJsonTo<T>(this string value)
	    {
	        if (value == null)
	        {
	            throw new ArgumentNullException("value");
	        }
	        return JsonConvert .DeserializeObject<T>(value);
	    }

	    public static string LimitToNullSafe(this string value, int length)
	    {
	        if (string.IsNullOrEmpty(value))
	        {
	            return value;
	        }
	        return (value.Length > length) ? value.Substring(0, length) : value;
	    }

	    public static string CutAt(this string value, int length)
	    {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
	        return (value.Length > length) ? string.Format("{0} (...)", value.Substring(0, length)) : value;
	    }

	    public static string HtmlFormatted(this string value)
	    {
	        return value == null ? null : value.Replace(Environment.NewLine, "<br/>");
	    }
	}
}