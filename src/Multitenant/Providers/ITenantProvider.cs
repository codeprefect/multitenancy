using System.Collections.Generic;
using Multitenant.Models;

namespace Multitenant.Providers
{
    public interface ITenantProvider
    {
        Tenant GetTenant();
        IEnumerable<Tenant> AllTenants { get; }
    }
}
