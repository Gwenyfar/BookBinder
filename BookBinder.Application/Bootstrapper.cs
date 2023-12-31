﻿using Autofac;
using BookBinder.Application.Commands;
using BookBinder.Application.Commands.CreateAuthor;
using BookBinder.Application.Queries;
using BookBinder.Infrastructure;
using BookBinder.Infrastructure.DataBaseConfiguration;
using BookBinder.Infrastructure.Mapping;
using BookBinder.Infrastructure.Repositories;
using BookBinder.Infrastructure.Repositories.Interfaces;
using BookBinder.Infrastructure.Security;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BookBinder.Application
{
    /// <summary>
    /// configures application dependencies 
    /// </summary>
    public class Bootstrapper
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="applicationSettings">settings extracted from secrets and environment variables</param>
        public Bootstrapper(ApplicationSettings applicationSettings)
        {
            ApplicationSettings = applicationSettings;
            Application = new Application();
            ConfigureContainer();
        }

        /// <summary>
        /// sets up all application dependencies
        /// </summary>
        private void ConfigureContainer()
        {
            var containerBuilder = new ContainerBuilder();
             new Configuration()
                        .Schema(Database.SCHEMA)
                        .ConnectionString(ApplicationSettings.ConnectionString)
                        .Mappings<UserMapping>()
                        .Migrations(typeof(UserMapping).Assembly)
                        .SetupDatabaseSchema(containerBuilder);

            InitialiseModule(ApplicationSettings, containerBuilder);

            Container = containerBuilder.Build();
            Application.Container = Container;
        }

        private Type FilterHandlers(Type type, Type handlerType)
        {
            var t = type
                .GetInterfaces();
            var sort = t.Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == handlerType);
               var result = sort .Single();
            return result;
        }

        /// <summary>
        /// registers all dependencies with the DI container
        /// </summary>
        /// <param name="applicationSettings">core app settings</param>
        /// <param name="containerBuilder">autofac's container builder</param>
        private void InitialiseModule(ApplicationSettings applicationSettings, ContainerBuilder containerBuilder)
        {
            var logger = applicationSettings.LoggerFactory.CreateLogger(AppName);
            logger.LogInformation($"{AppName} is initialising..");

            containerBuilder.Register((o) => logger)
                .As<ILogger>().InstancePerLifetimeScope();

            RegisterServices(containerBuilder);

            containerBuilder.RegisterType<Dbcontext>()
                .As<Dbcontext>().PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(ApplicationAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(CommandHandler<,>)))
                .As(t => FilterHandlers(t, typeof(ICommandHandler<,>)))
                .AsClosedTypesOf(typeof(CommandHandler<,>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(ApplicationAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(CommandHandler<>)))
                .As(t => FilterHandlers(t, typeof(ICommandHandler<>)))
                .AsClosedTypesOf(typeof(CommandHandler<>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

            containerBuilder.RegisterAssemblyTypes(ApplicationAssembly)
                .Where(t => t.IsClosedTypeOf(typeof(QueryHandler<,>)))
                .As(t => FilterHandlers(t, typeof(IQueryHandler<,>)))
                .AsClosedTypesOf(typeof(QueryHandler<,>))
                .PropertiesAutowired()
                .InstancePerLifetimeScope();
        }

        /// <summary>
        /// registers repository types with the DI container
        /// </summary>
        /// <param name="containerBuilder">autofac's container builder</param>
        private void RegisterServices(ContainerBuilder containerBuilder)
        {
            
            containerBuilder.RegisterType<AuthorRepository>()
                .As<IAuthorRepository>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<PublisherRepository>()
                .As<IPublisherRepository>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<PasswordManager>()
                .As<IPasswordManager>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<TokenProvider>()
                .As<ITokenProvider>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<AccessManager>()
                .As<IAccessManager>()
                .PropertiesAutowired()
                .InstancePerLifetimeScope();

        }
        /// <summary>
        /// application settings
        /// </summary>
        public ApplicationSettings ApplicationSettings { get; set; }
        /// <summary>
        /// DI container
        /// </summary>
        public IContainer Container { get; private set; }
        /// <summary>
        /// Application instance
        /// </summary>
        public Application Application { get; set; }
        public string AppName => "BookBinder";
        /// <summary>
        /// assembly where commands and queries reside
        /// </summary>
        public Assembly ApplicationAssembly => typeof(CreateAuthorCommand).Assembly;
    }
}
