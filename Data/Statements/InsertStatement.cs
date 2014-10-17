using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using Skattedugnad.Data.Attributes;

namespace Skattedugnad.Data.Statements
{
    public class InsertStatement
    {
        private static readonly IDictionary<TypeAndTable, InsertStatement> InsertStatements = new Dictionary<TypeAndTable, InsertStatement>();

        private readonly TypeAndTable _typeAndTable;
        private readonly PropertyInfo[] _properties;
        public string CommandText { get; private set; }

        public static InsertStatement For<T>()
        {
            return For<T>(typeof (T).Name);
        }

        public static InsertStatement For<T>(string tableName)
        {
            return For(new TypeAndTable(typeof (T), tableName));
        }

        public static InsertStatement For(TypeAndTable key)
        {
            if (!InsertStatements.ContainsKey(key))
            {
                InsertStatements[key] = new InsertStatement(key);
            }
            return InsertStatements[key];
        }

        private InsertStatement(TypeAndTable typeAndTable)
        {
            _typeAndTable = typeAndTable;
            _properties = typeAndTable.Type.GetProperties().Where(p => p.Name!="Id").ToArray();
            var parameterNames = _properties.Select(ParameterNameFor);
            var columnNames = _properties.Select(p => string.Format("[{0}]", p.Name));
            CommandText = string.Format("insert into [{0}] ({1}) values ({2})", _typeAndTable.TableName, string.Join(", ", columnNames), string.Join(", ", parameterNames));
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