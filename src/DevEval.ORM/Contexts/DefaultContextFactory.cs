using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DevEval.ORM.Contexts
{
    /// <summary>
    /// Factory for creating an instance of DefaultContext during design-time.
    /// </summary>
    public class DefaultContextFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        public DefaultContext CreateDbContext(string[] args)
        {
            // Load configuration settings
            var configuration = LoadConfiguration();

            var optionsBuilder = new DbContextOptionsBuilder<DefaultContext>();

            // Configure database provider (PostgreSQL in this case)
            var connectionString = configuration.GetConnectionString("PostgreSqlConnection");

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException("Connection string 'PostgreSqlConnection' is missing in the configuration.");
            }

            optionsBuilder.UseNpgsql(connectionString);

            return new DefaultContext(optionsBuilder.Options);
        }

        /// <summary>
        /// Loads the application configuration for design-time use.
        /// </summary>
        private IConfiguration LoadConfiguration()
        {
            // Define the base path to the WebAPI project directory
            var basePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())?.FullName ?? string.Empty, "DevEval.WebApi");

            if (!Directory.Exists(basePath))
            {
                throw new DirectoryNotFoundException($"The base path '{basePath}' does not exist. Ensure it points to the DevEval.WebApi directory.");
            }

            return new ConfigurationBuilder()
                .SetBasePath(basePath) // Set the base path to the WebApi directory
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true) // Load appsettings.json
                .AddJsonFile("appsettings.Development.json", optional: true) // Load appsettings.Development.json if exists
                .AddEnvironmentVariables() // Load environment variables
                .Build();
        }
    }
}
