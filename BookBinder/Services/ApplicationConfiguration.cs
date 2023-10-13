using BookBinder.Infrastructure;

namespace BookBinder.Services
{
    /// <summary>
    /// fetches the core application settings
    /// </summary>
    public static class ApplicationConfiguration
    {
        /// <summary>
        /// fetches the application's connection string
        /// </summary>
        /// <param name="configuration">a model representing the environment variables of a machine</param>
        /// <returns>a connection string</returns>
        private static string ExtractSQLConnectionString(IConfiguration configuration)
        {
            var connectionString = configuration["CCL:SQLDB"];
            return connectionString;
        }

        /// <summary>
        /// extracts all app settings
        /// </summary>
        /// <param name="configuration">environment variables and secrets</param>
        /// <param name="loggerFactory">creates loggers</param>
        /// <returns>all core appllication settings</returns>
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
