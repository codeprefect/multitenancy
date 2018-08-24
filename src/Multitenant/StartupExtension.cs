using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Multitenant.Providers;
using MultiTenant.Data.Interfaces;

namespace Multitenant
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
    }
}
