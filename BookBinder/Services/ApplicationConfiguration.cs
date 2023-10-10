using BookBinder.Infrastructure;

namespace BookBinder.Services
{
    public static class ApplicationConfiguration
    {
        private static string ExtractSQLConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration["CCL:SQLDB"];
            return connectionString;
        }

        public static ApplicationSettings ExtractAppSettings(this IConfiguration configuration, ILoggerFactory loggerFactory)
        {
            return new ApplicationSettings
            {
                ConnectionString = ExtractSQLConnectionString(configuration),
                LoggerFactory = loggerFactory
            };
        }
    }
}
