//using System;
//using System.IO;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Design;
//using Microsoft.Extensions.Configuration;

//namespace PensionManager.PensionManager.Infrastructure
//{
//    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
//    {
//        public ApplicationDbContext CreateDbContext(string[] args)
//        {
//            // Determine the environment (e.g., Development, Production)
//            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Development";

//            // Build the configuration by loading appsettings.json and an optional environment-specific file.
//            var configuration = new ConfigurationBuilder()
//                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure you have using System.IO;
//                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
//                .AddJsonFile($"appsettings.{environment}.json", optional: true)
//                .AddEnvironmentVariables()
//                .Build();

//            // Retrieve the connection string from configuration
//            var connectionString = configuration.GetConnectionString("DefaultConnection");

//            // Set up the DbContext options using the retrieved connection string.
//            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
//            optionsBuilder.UseSqlServer(connectionString);

//            return new ApplicationDbContext(optionsBuilder.Options);
//        }
//    }
//}
