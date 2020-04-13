using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multitenant.Data;
using Multitenant.Middlewares;
using Multitenant.Models;
using MultiTenant.Data.Interfaces;

namespace Multitenant.Extensions
{
    public static class StartupExtension
    {
        public static void EnsureMigrationsRun(this IApplicationBuilder app, IConfiguration configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContextFactory = serviceScope.ServiceProvider.GetService<IDbContextFactory>();
                var allTenants = serviceScope.ServiceProvider.GetService<ITenantProvider>().AllTenants;
                foreach (var tenant in allTenants)
                {
                    var context = dbContextFactory.CreateDbContext(tenant, configuration);
                    context.Database.Migrate();
                }
            }
        }

        public static void AddDefaultDatabaseContext(this IServiceCollection services, IConfiguration configuration)
        {
            var defaultDbConfig = configuration.GetSection("DefaultDatabase");
            string connectionString = defaultDbConfig["ConnectionString"];

            if (defaultDbConfig["Provider"] == DatabaseProvider.SQLITE.ToString())
            {
                connectionString = DbContextExtensions.ReconstructSqliteConnectionString(connectionString);
            }

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(connectionString));
        }
    }
}
