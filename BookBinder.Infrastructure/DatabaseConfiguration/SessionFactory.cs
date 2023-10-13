using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using config = NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BookBinder.Infrastructure.DataBaseConfiguration
{
    /// <summary>
    /// provides sessions
    /// </summary>
    public class SessionFactory : INhibernateSessionFactory
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="connectionString">the connection string</param>
        /// <param name="schema">schema name</param>
        /// <param name="assembly">mappings assembly</param>
        public SessionFactory(string connectionString, string schema, Assembly assembly)
        {
            _connectionString = connectionString;
            Schema = schema;
            Assembly = assembly;
            Factory = BuildSessionFactory();
        }

        /// <summary>
        /// configures and builds a session factory
        /// </summary>
        /// <returns>a session factory</returns>
        private ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure().Database(MsSQL)
                .Mappings((m) => m.FluentMappings.AddFromAssembly(Assembly))
                .ExposeConfiguration(ConfigureSchemas())
                .BuildSessionFactory();
        }

        /// <summary>
        /// sets sql database configurations
        /// </summary>
        /// <returns>sql configuration</returns>
        private MsSqlConfiguration MsSQL()
        {
            return MsSqlConfiguration.MsSql2012.ShowSql().DefaultSchema(Schema)
                                               .ConnectionString(_connectionString);
        }

        /// <summary>
        /// configure schema update
        /// </summary>
        /// <returns>an action for the configuration</returns>
        private Action<config.Configuration> ConfigureSchemas()
        {
            return cfg =>
            {
                var schema = new SchemaUpdate(cfg);
                schema.Execute(true, true);
                cfg.SetProperty(config.Environment.CommandTimeout, "2000");
            };
        }

        /// <summary>
        /// gets the session from session factory
        /// </summary>
        /// <returns></returns>
        public ISession GetSession()
        {
            return Factory.OpenSession();
        }
        /// <summary>
        /// connection string
        /// </summary>
        private readonly string _connectionString;
        /// <summary>
        /// database schema
        /// </summary>
        public string Schema { get; set; }
        /// <summary>
        /// mappings assembly
        /// </summary>
        public Assembly Assembly { get; set; }
        /// <summary>
        /// session factory
        /// </summary>
        public ISessionFactory Factory { get; }
    }
}
