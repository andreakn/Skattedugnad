using System.Collections.Specialized;

namespace Skattedugnad.Utilities
{
    public static class NameValueCollectionExtensions
    {
        public static T GetOrDefault<T>(this NameValueCollection collection, string key, T defaultValue = default(T))
        {
            try
            {
                var value = collection[key];
                return value == null
                    ? defaultValue
                    : value.ConvertTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}