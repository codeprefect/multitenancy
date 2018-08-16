using Microsoft.Extensions.Configuration;
using Multitenant.Data;
using Multitenant.Models;

public interface IDbContextFactory
{
    ApplicationDbContext CreateDbContext(Tenant tenant, IConfiguration confifuration);
}
