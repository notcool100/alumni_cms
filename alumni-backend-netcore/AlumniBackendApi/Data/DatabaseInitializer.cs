using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace AlumniBackendApi.Data
{
    public static class DatabaseInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AlumniContext>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<AlumniContext>>();

            try
            {
                // Ensure database is created
                context.Database.EnsureCreated();
                logger.LogInformation("✅ Database initialized successfully");

                // Check if we have any data
                if (!context.Users.Any())
                {
                    logger.LogInformation("📝 No users found, database is ready for first use");
                }
                else
                {
                    logger.LogInformation("📊 Database contains {UserCount} users", context.Users.Count());
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ Error initializing database");
                throw;
            }
        }
    }
}
