using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Skattedugnad.Data
{
    public interface ISqlDatabase
    {
        int Execute(string sql, object parameters = null);
        int Insert<T>(T item);
        int InsertInto<T>(string table, T item);
        IEnumerable<T> Query<T>(string sql, object parameters = null);
        object ExecuteScalar(string sql, object parameters = null);
        T ExecuteScalarOrDefault<T>(string sql, object parameters = null);
        int Update<T>(T item, params Expression<Func<T, object>>[] properties) where T : IHaveId;
    }
}