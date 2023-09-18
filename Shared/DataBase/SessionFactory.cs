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

namespace BookBinder.Infrastructure.DataBase
{
    public class SessionFactory : INhibernateSessionFactory
    {
        public SessionFactory(string connectionString, string schema, Assembly assembly)
        {
            _connectionString = connectionString;
            Schema = schema;
            Assembly = assembly;
            Factory = BuildSessionFactory();
        }

        private ISessionFactory BuildSessionFactory()
        {
            return Fluently.Configure().Database(MsSQL)
                .Mappings((m) => m.FluentMappings.AddFromAssembly(Assembly))
                .ExposeConfiguration(ConfigureSchemas())
                .BuildSessionFactory();
        }

        private MsSqlConfiguration MsSQL()
        {
            return MsSqlConfiguration.MsSql2012.ShowSql().DefaultSchema(Schema)
                                               .ConnectionString(_connectionString);
        }

        private Action<config.Configuration> ConfigureSchemas()
        {
            return cfg =>
            {
                var schema = new SchemaUpdate(cfg);
                schema.Execute(true, true);
                cfg.SetProperty(NHibernate.Cfg.Environment.CommandTimeout, "2000");
            };
        }

        public ISession GetSession()
        {
            return Factory.OpenSession();
        }

        private readonly string _connectionString;
        public string Schema { get; set; }
        public Assembly Assembly { get; set; }
        public ISessionFactory Factory { get; }
    }
}
