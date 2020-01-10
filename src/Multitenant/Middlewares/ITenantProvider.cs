using System.Collections.Generic;
using Multitenant.Models;

namespace Multitenant.Middlewares
{
    public interface ITenantProvider
    {
        Tenant GetTenant();
        IEnumerable<Tenant> AllTenants { get; }
    }
}
