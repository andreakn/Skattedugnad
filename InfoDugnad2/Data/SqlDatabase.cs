using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using Dapper;
using Skattedugnad.Data.Statements;
using Skattedugnad.Utilities;
using Skattedugnad.Utilities.Exceptions;

namespace Skattedugnad.Data
{
    public class SqlDatabase : ISqlDatabase
    {
        private readonly string _connectionString;
        private readonly bool _exposeSqlInExceptions;

        public SqlDatabase(string connectionString, bool exposeSqlInExceptions)
        {
            _connectionString = connectionString;
            _exposeSqlInExceptions = exposeSqlInExceptions;
        }

        public int Execute(string sql, object parameters = null)
        {
            using (var connection = CreateConnection())
            {
                return connection.Execute(sql, parameters);
            }
        }

        public int Insert<T>(T item)
        {
            return InsertInto(typeof (T).Name, item);
        }

        public int InsertInto<T>(string table, T item)
        {
            using (var connection = CreateConnection())
            {
                var statement = InsertStatement.For<T>(table);
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = statement.CommandText;
                    var parameters = statement.GetParametersFor(item).ToArray();
                    command.Parameters.AddRange(parameters);
                    connection.Open();
                    try
                    {
                        return command.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        if (_exposeSqlInExceptions)
                        {
                            var parameterStrings = parameters.Select(p => string.Format("{0}={1}", p.ParameterName, p.Value));
                            throw new TechnicalException(
                                string.Format("Sql failed: {0} ({1})", statement.CommandText,
                                    string.Join(", ", parameterStrings)), ex);
                        }
                        throw;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public int Update<T>(T item, params Expression<Func<T, object>>[] properties) where T : IHaveId
        {
            var statement = UpdateStatement.For(properties);
            using (var connection = CreateConnection())
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = statement.CommandText;
                    command.Parameters.AddRange(statement.GetParametersFor(item).ToArray());
                    connection.Open();
                    var result = command.ExecuteNonQuery();
                    connection.Close();
                    return result;
                }
            }
        }

        public object ExecuteScalar(string sql, object parameters = null)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = sql;
                        if (parameters != null)
                        {
                            var usedParameters = from v in parameters.ToPropertyDictionary()
                                let name = ParameterName(v.Key)
                                where sql.Contains(name)
                                select new SqlParameter(name, v.Value);
                            command.Parameters.AddRange(usedParameters.ToArray());
                        }
                        connection.Open();
                        var value = command.ExecuteScalar();
                        connection.Close();
                        return value;
                    }
                }
            }
            catch (Exception ex)
            {
                if (_exposeSqlInExceptions)
                {
                    var parameterValues = parameters == null
                        ? new string[0]
                        : parameters.ToPropertyDictionary()
                            .Select(v => string.Format("{0}={1}", v.Key, v.Value))
                            .ToArray();
                    throw new TechnicalException(string.Format("Sql failed: {0} ({1})", sql, string.Join(", ", parameterValues)), ex);
                }
                throw;
            }
        }

        private static string ParameterName(string property)
        {
            return string.Format("@{0}", property);
        }

        public T ExecuteScalarOrDefault<T>(string sql, object parameters = null)
        {
            var value = ExecuteScalar(sql, parameters);
            if (value.IsNullOrDbNull())
            {
                return default(T);
            }
            return (T) value;
        }

        public IEnumerable<T> Query<T>(string sql, object parameters = null)
        {
            try
            {
                using (var connection = CreateConnection())
                {
                    connection.Open();
                    var items = Enumerable.ToList<T>(connection.Query<T>(sql, parameters));
                    connection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                if (_exposeSqlInExceptions)
                {
                    var parameterValues = parameters == null
                        ? new string[0]
                        : parameters.ToPropertyDictionary()
                            .Select(v => string.Format("{0}={1}", v.Key, v.Value))
                            .ToArray();
                    throw new TechnicalException(string.Format("Sql failed: {0} ({1})", sql, string.Join(", ", parameterValues)), ex);
                }
                throw;
            }
        }

        private SqlConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}