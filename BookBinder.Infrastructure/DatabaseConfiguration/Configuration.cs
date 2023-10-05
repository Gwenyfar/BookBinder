using Autofac;
using BookBinder.Infrastructure.Utilities;
using System.Reflection;

namespace BookBinder.Infrastructure.DataBaseConfiguration
{
    public class Configuration
    {
        public Configuration Schema(string schema)
        {
            _schema = schema;
            return this;
        }

        public Configuration ConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        public Configuration Mappings<T>()
        {
            _mappingAssembly = typeof(T).Assembly;
            return this;
        }
        public Configuration Migrations(Assembly assembly)
        {
            _migrationsAssembly = assembly;
            return this;
        }

        public void SetupDatabaseSchema(ContainerBuilder containerBuilder)
        {
            if (_schema != null)
                Database.CreateSchema(_connectionString, _schema);
            if (_migrationsAssembly != null)
                RunMigrations();
            if (_mappingAssembly != null)
                Database.Configure(_mappingAssembly, _schema, containerBuilder, _connectionString);

        }

        private void RunMigrations()
        {
            try
            {
                new MigrationRunner(_migrationsAssembly, _connectionString).Run();
            }
            catch (Exception)
            {
                throw;
            }
        }
        private string _schema;
        private string _connectionString;
        private Assembly _mappingAssembly;
        private Assembly _migrationsAssembly;
    }
}
