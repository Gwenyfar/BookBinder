﻿using Autofac;
using BookBinder.Application.Data.Mappings;
using BookBinder.Application.Data.Repositories;
using BookBinder.Infrastructure;
using BookBinder.Infrastructure.DataBase;
using BookBinder.Infrastructure.Repository;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BookBinder.Application.Services
{
    public class Bootstrapper
    {
        public Bootstrapper(ApplicationSettings applicationSettings)
        {
            ApplicationSettings = applicationSettings;
            ConfigureContainer();
        }

        private void ConfigureContainer()
        {
            var containerBuilder = new ContainerBuilder();
             new Configuration()
                        .Schema(SCHEMA)
                        .ConnectionString(ApplicationSettings.ConnectionString)
                        .Mappings<UserMapping>()
                        .Migrations(typeof(UserMapping).Assembly)
                        .SetupDatabaseSchema(containerBuilder);

            InitialiseModule(ApplicationSettings, containerBuilder);

            Container = containerBuilder.Build();
        }

        private void InitialiseModule(ApplicationSettings applicationSettings, ContainerBuilder containerBuilder)
        {
            var logger = applicationSettings.LoggerFactory.CreateLogger(AppName);
            logger.LogInformation($"{AppName} is initialising..");

            containerBuilder.Register((o) => logger)
                .As<ILogger>().InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(ApplicationAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(Repository<>)))
                .As<IRespository>()
                .AsClosedTypesOf(typeof(Repository<>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
            
        }

        public ApplicationSettings ApplicationSettings { get; set; }
        public IContainer Container { get; private set; }
        public string AppName => "BookBinder";
        public const string SCHEMA  = "BookBinder";
        public Assembly ApplicationAssembly => typeof(AuthorRepository).Assembly;
    }
}
