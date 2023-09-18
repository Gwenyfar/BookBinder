using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.Migrations
{
    internal class Runner
    {
        public Runner(Assembly migrationsAssembly, string connectionString)
        {
            _connectionString = connectionString;
            _migrationsAssembly = migrationsAssembly;
            _serviceProvider = CreateMigratorService();

        }

        internal void Run()
        {
            using var scope = _serviceProvider.CreateScope();
            var runner = scope.ServiceProvider.GetService<IMigrationRunner>();
            runner.MigrateUp();
        }
        private IServiceProvider CreateMigratorService()
        {
            var service = new ServiceCollection()
                          .AddFluentMigratorCore()
                          .ConfigureRunner(BuildConnection())
                          .BuildServiceProvider();
            return service;
        }

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
