using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Skattedugnad.Data.Attributes;
using Skattedugnad.Utilities;
using System.Reflection;
namespace Skattedugnad.Data.Statements
{
    public class UpdateStatement
    {
        private readonly TypeAndTable _typeAndTable;
        private readonly PropertyInfo[] _properties;

        public string CommandText { get; private set; }

        private UpdateStatement(TypeAndTable typeAndTable, ICollection<string> propertyNames)
        {
            _typeAndTable = typeAndTable;
            _properties = typeAndTable.Type.GetProperties()
                .Where(p => p.Name != "Id"  && (propertyNames.Count == 0 || propertyNames.Contains(p.Name)))
                .ToArray();

            var updates = _properties.Select(p => string.Format("[{0}]={1}", p.Name, ParameterNameFor(p)));
            CommandText = string.Format("update [{0}] set {1} where Id=@Id", _typeAndTable.TableName, string.Join(", ", updates));
        }

        public static UpdateStatement For<T>(params Expression<Func<T, object>>[] properties)
        {
            var propertyNames = properties.Select(p => p.GetPropertyName());
            return new UpdateStatement(new TypeAndTable(typeof(T), typeof(T).Name), propertyNames.ToList());
        }

        private static string ParameterNameFor(PropertyInfo property)
        {
            return string.Format("@{0}", property.Name);
        }

        public IEnumerable<SqlParameter> GetParametersFor(object item)
        {
            if (item == null)
            {
                throw new ArgumentNullException("item");
            }
            if (item.GetType() != _typeAndTable.Type)
            {
                throw new ArgumentException(string.Format("{0} must be of type {1}", item.GetType(), _typeAndTable.Type));
            }
            return _properties.Select(p => new SqlParameter(ParameterNameFor(p), GetSqlValue(p, item)));
        }

        private static object GetSqlValue(PropertyInfo property, object item)
        {
            var value = property.GetValue(item, new object[0]);
            return value ?? DBNull.Value;
        }
    }
}