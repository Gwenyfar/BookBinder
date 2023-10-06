using BookBinder.Infrastructure;

namespace BookBinder.Services
{
    public static class ApplicationConfiguration
    {
        private static string ExtractSQLConnectionString(this IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("CCL").GetValue<string>("SQLDB");
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
