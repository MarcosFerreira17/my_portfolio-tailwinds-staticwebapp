using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using MWF.Projects.Application.Exceptions;
using MWF.Projects.Infraestructure.DataContext;

namespace MWF.Projects.Host.Configurations.Extensions;

public static class HostExtensions
{
    public static IHost MigrateDatabase<TContext>(this IHost host) where TContext : MWF.ProjectsDbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var logger = services.GetRequiredService<ILogger<TContext>>();
            var context = services.GetService<TContext>();

            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);

                var retry = Policy.Handle<Exception>()
                        .WaitAndRetry(
                            retryCount: 5,
                            sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), // 2,4,8,16,32 sc
                            onRetry: (exception, retryCount, context) => logger.LogError($"Retry {retryCount} of {context.PolicyKey} at {context.OperationKey}, due to: {exception}."));

                retry.Execute(() => InvokeSeeder(context));

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                throw new GenericException(ex.Message + "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
            }
        }
        return host;
    }
    private static void InvokeSeeder<TContext>(TContext context)
        where TContext : MWF.ProjectsDbContext => context.Database.Migrate();
}