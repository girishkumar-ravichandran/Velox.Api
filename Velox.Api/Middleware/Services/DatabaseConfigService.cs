namespace Velox.Api.Middleware.Services
{
    public class DatabaseConfigService
    {
        private readonly string _connectionString;

        public DatabaseConfigService(IConfiguration configuration)
        {
            // Read the flag from configuration
            bool useHostedDB = configuration.GetValue<bool>("UseHostedDB");

            // Select the appropriate connection string
            _connectionString = useHostedDB
                ? configuration.GetConnectionString("HostedDB")
                : configuration.GetConnectionString("LocalDB");
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }
    }

}
