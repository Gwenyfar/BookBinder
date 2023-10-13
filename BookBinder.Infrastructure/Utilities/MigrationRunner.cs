using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookBinder.Infrastructure.Utilities
{
    /// <summary>
    /// runs migrations
    /// </summary>
    internal class MigrationRunner
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="migrationsAssembly">migrations assembly</param>
        /// <param name="connectionString">connection string</param>
        public MigrationRunner(Assembly migrationsAssembly, string connectionString)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationsAssembly;
            _serviceProvider = CreateMigratorService();

        }
        /// <summary>
        /// runs migrations
        /// </summary>
        internal void Run()
        {
            using var scope = _serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            runner.MigrateUp();
        }
        /// <summary>
        /// creates and registers a migration runner as a service
        /// </summary>
        /// <returns>the service</returns>
        private IServiceProvider CreateMigratorService()
        {
            var service = new ServiceCollection()
                          .AddFluentMigratorCore()
                          .ConfigureRunner(BuildConnection())
                          .BuildServiceProvider();
            return service;
        }

        /// <summary>
        /// configures a migration runner
        /// </summary>
        /// <returns>a migration runner builder</returns>
        private Action<IMigrationRunnerBuilder> BuildConnection()
        {
            return builder => builder.AddSqlServer2012()
                                     .WithGlobalConnectionString(_connectionString)
                                     .ScanIn(_migrationsAssembly).For.Migrations();
        }
        private readonly Assembly _migrationsAssembly;
        private readonly string _connectionString;
        private readonly IServiceProvider _serviceProvider;
    }
}
