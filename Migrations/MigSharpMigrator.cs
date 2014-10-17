using System.Data.SqlClient;
using System.Reflection;
using MigSharp;
using Skattedugnad.Utilities;

namespace Skattedugnad.Migrations
{
    public class MigSharpMigrator
    {
        private readonly string _connectionString;
        private readonly Assembly _assembly;

        public MigSharpMigrator(string connectionString, Assembly assembly)
        {
            _connectionString = connectionString;
            _assembly = assembly;
        }

        public void MigrateUp()
        {
            var migrator = GetMigrator();
            migrator.MigrateAll(_assembly);
        }

        private Migrator GetMigrator()
        {
            var migrationOptions = new MigrationOptions();
            migrationOptions.SupportedProviders.Set(new[] { ProviderNames.SqlServer2008 });
            var migrator = new Migrator(_connectionString, ProviderNames.SqlServer2008, migrationOptions);
            return migrator;
        }

        public void MigrateDown()
        {
            MigrateTo(1);
        }

        public void MigrateTo(long version)
        {
            var migrator = GetMigrator();
            migrator.MigrateTo(_assembly, version);
        }

        public long GetCurrentVersion()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select max(Timestamp) from MigSharp";
                    connection.Open();
                    var value = command.ExecuteScalar();
                    connection.Close();
                    return value.IsNullOrDbNull() ? 0 : (long) value;
                }
            }
        }
    }
}