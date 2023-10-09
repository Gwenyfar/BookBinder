using BookBinder.Infrastructure;

namespace BookBinder.Services
{
    public static class ApplicationConfiguration
    {
        private static string ExtractSQLConnectionString(this IConfiguration configuration)
        {
            var connectionString = configuration["CCL:SQLDB"];
            //var connectionString = config.GetConnectionString("SQLDB");
            return connectionString;
        }

        public static ApplicationSettings ExtractAppSettings(this IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            return new ApplicationSettings
            {
                ConnectionString = configuration.ExtractSQLConnectionString(),
                LoggerFactory = loggerFactory
            };
        }
    }
}
