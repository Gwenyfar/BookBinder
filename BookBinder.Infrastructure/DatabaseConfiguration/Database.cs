using Autofac;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.DataBaseConfiguration
{
    /// <summary>
    /// connects to the database
    /// </summary>
    public static class Database
    {
        /// <summary>
        /// registers the session and session factory with the DI container
        /// </summary>
        /// <param name="mappingAssembly">mapping assembly</param>
        /// <param name="schema">schema name</param>
        /// <param name="builder">container builder</param>
        /// <param name="connectionString">connection string</param>
        internal static void Configure(Assembly mappingAssembly, string schema, 
            ContainerBuilder builder, string connectionString)
        {
            var sessionFactory = new SessionFactory(connectionString, schema, mappingAssembly);

            builder.Register((o) => sessionFactory)
                .As<INhibernateSessionFactory>()
                .SingleInstance();

            builder.Register((o) =>
            {
                var factory = o.Resolve<INhibernateSessionFactory>();
                return factory.GetSession();
            }).As<ISession>()
            .InstancePerLifetimeScope();
        }

        /// <summary>
        /// Creates database schema
        /// </summary>
        /// <param name="connectionString">the connection string</param>
        /// <param name="schema">schema name</param>
        internal static void CreateSchema(string connectionString, string schema)
        {
            var query = $"CREATE SCHEMA [{schema}] AUTHORIZATION [dbo]";

            try
            {
                using var connection = new SqlConnection(connectionString);
                using var command = new SqlCommand(query, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch(SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public const string SCHEMA = "BookBinder";

    }
}
