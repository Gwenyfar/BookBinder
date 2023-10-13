using Autofac;
using BookBinder.Infrastructure.Utilities;
using System.Reflection;

namespace BookBinder.Infrastructure.DataBaseConfiguration
{
    /// <summary>
    /// sets up database
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// sets up schema
        /// </summary>
        /// <param name="schema">schema name</param>
        /// <returns>this configuration object</returns>
        public Configuration Schema(string schema)
        {
            _schema = schema;
            return this;
        }

        /// <summary>
        /// gets the connection string
        /// </summary>
        /// <param name="connectionString">the connection string</param>
        /// <returns>this configuration object</returns>
        public Configuration ConnectionString(string connectionString)
        {
            _connectionString = connectionString;
            return this;
        }

        /// <summary>
        /// gets mapping assembly
        /// </summary>
        /// <typeparam name="T">assembly type</typeparam>
        /// <returns>this configuration object</returns>
        public Configuration Mappings<T>()
        {
            _mappingAssembly = typeof(T).Assembly;
            return this;
        }

        /// <summary>
        /// gets migration assembly
        /// </summary>
        /// <param name="assembly">assembly type</param>
        /// <returns>this configuration object</returns>
        public Configuration Migrations(Assembly assembly)
        {
            _migrationsAssembly = assembly;
            return this;
        }

        /// <summary>
        /// sets up database
        /// </summary>
        /// <param name="containerBuilder">autofac's container builder</param>
        public void SetupDatabaseSchema(ContainerBuilder containerBuilder)
        {
            if (_schema != null)
                Database.CreateSchema(_connectionString, _schema);
            if (_migrationsAssembly != null)
                RunMigrations();
            if (_mappingAssembly != null)
                Database.Configure(_mappingAssembly, _schema, containerBuilder, _connectionString);

        }

        /// <summary>
        /// runs migrations
        /// </summary>
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
