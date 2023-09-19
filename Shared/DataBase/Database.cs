using Autofac;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.DataBase
{
    internal static class Database
    {
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

    }
}
