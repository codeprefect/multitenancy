
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Multitenant.Models;
using MultiTenant.Data.Interfaces;

namespace Multitenant.Data
{
    public class DbContextFactory : IDbContextFactory
    {
        public ApplicationDbContext CreateDbContext(Tenant tenant, IConfiguration configuration)
        {
            var options = new DbContextOptions<ApplicationDbContext>();
            var dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>(options);
            return new ApplicationDbContext(dbContextOptionsBuilder.Options, configuration, tenant);
        }
    }
}
