using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Skattedugnad.Utilities.Exceptions;

namespace Skattedugnad.Utilities
{
    public static class ObjectExtensions
    {
        public static T ConvertToOrDefault<T>(this object item, T defaultValue = default(T))
        {
            try
            {
                return item.ConvertTo<T>();
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T ConvertTo<T>(this object item)
        {
            return (T) item.ConvertTo(typeof(T));
        }

        public static object ConvertTo(this object item, Type type)
        {
            try
            {
                if (Nullable.GetUnderlyingType(type) != null)
                {
                    return item == null ? null : item.ConvertTo(type.GetGenericArguments()[0]);
                }
                if (item == null)
                {
                    throw new ArgumentNullException("item");
                }
                if (type == typeof(Guid))
                {
                    return Guid.Parse(item.ToString());
                }
                if (type == typeof(DateTime))
                {
                    return DateTime.Parse(item.ToString());
                }
                if (type == typeof(bool))
                {
                    return bool.Parse(item.ToString());
                }
                if (type.IsEnum)
                {
                    return Enum.Parse(type, item.ToString());
                }
                return Convert.ChangeType(item, type);
            }
            catch (Exception ex)
            {
                throw new TechnicalException(string.Format("Cannot convert '{0}' to {1}", item, type), ex);
            }
        }

        public static IDictionary<string, object> ToPropertyDictionary(this object item)
        {
            var dictionary = new Dictionary<string, object>();
            if (item == null)
            {
                return dictionary;
            }
            foreach (var property in item.GetType().GetProperties())
            {
                dictionary[property.Name] = property.GetValue(item, new object[0]);
            }
            return dictionary;
        }

        public static string ToJson(this object item, bool indented = false, bool suppressErrors = false)
        {
            var formatting = indented ? Formatting.Indented : Formatting.None;
            var settings = new JsonSerializerSettings();
            if (suppressErrors)
            {
                settings.Error = SuppressJsonSerializerError;
            }
            return JsonConvert.SerializeObject(item, formatting, settings);
        }

        private static void SuppressJsonSerializerError(object sender, ErrorEventArgs e)
        {
            e.ErrorContext.Handled = true;
        }

        public static List<T> InList<T>(this T item)
        {
            return new List<T>{item};
        }
    }
}