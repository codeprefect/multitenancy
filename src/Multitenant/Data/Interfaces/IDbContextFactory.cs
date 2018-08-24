using Microsoft.Extensions.Configuration;
using Multitenant.Data;
using Multitenant.Models;

namespace MultiTenant.Data.Interfaces
{
    public interface IDbContextFactory
    {
        ApplicationDbContext CreateDbContext(Tenant tenant, IConfiguration confifuration);
    }
}
