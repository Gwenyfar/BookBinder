using Autofac;
using BookBinder.Application.Commands;
using BookBinder.Application.Commands.CreateAuthor;
using BookBinder.Application.Queries;
using BookBinder.Infrastructure;
using BookBinder.Infrastructure.DataBaseConfiguration;
using BookBinder.Infrastructure.Mapping;
using BookBinder.Infrastructure.Repositories;
using BookBinder.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace BookBinder.Application
{
    public class Bootstrapper
    {
        public Bootstrapper(ApplicationSettings applicationSettings)
        {
            ApplicationSettings = applicationSettings;
            Application = new Application();
            ConfigureContainer();
        }

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
        private void InitialiseModule(ApplicationSettings applicationSettings, ContainerBuilder containerBuilder)
        {
            var logger = applicationSettings.LoggerFactory.CreateLogger(AppName);
            logger.LogInformation($"{AppName} is initialising..");

            containerBuilder.Register((o) => logger)
                .As<ILogger>().InstancePerLifetimeScope();

            RegisterRepositories(containerBuilder);

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

        private void RegisterRepositories(ContainerBuilder containerBuilder)
        {
            
            containerBuilder.RegisterType<AuthorRepository>()
                .As<IAuthorRepository>().InstancePerLifetimeScope();

            containerBuilder.RegisterType<PublisherRepository>()
                .As<IPublisherRepository>().InstancePerLifetimeScope();
        }
        public ApplicationSettings ApplicationSettings { get; set; }
        public IContainer Container { get; private set; }
        public Application Application { get; set; }
        public string AppName => "BookBinder";
        
        public Assembly ApplicationAssembly => typeof(CreateAuthorCommand).Assembly;
    }
}
