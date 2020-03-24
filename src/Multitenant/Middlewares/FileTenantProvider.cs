using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Multitenant.Models;
using Newtonsoft.Json;

namespace Multitenant.Middlewares
{
    public class FileTenantProvider : ITenantProvider
    {
        private static IList<Tenant> _tenants;
        private Tenant _tenant;

        public IEnumerable<Tenant> AllTenants { get => _tenants; }
        public FileTenantProvider(IHttpContextAccessor accessor)
        {
            if (_tenants == null)
            {
                LoadTenants();
            }

            var host = accessor?.HttpContext?.Request?.Host.Value ?? "";
            if (string.IsNullOrEmpty(host))
            {
                _tenant = _tenants.FirstOrDefault(t => t.Id == 1);
            }
            else
            {
                var tenant = _tenants.FirstOrDefault(t => t.Host.ToLower() == host.ToLower());
                if (tenant != null) _tenant = tenant;
            }
        }

        public Tenant GetTenant()
        {
            return _tenant;
        }

        public void LoadTenants()
        {
            _tenants = DeSerializeNonStandardList();
        }

        public IList<Tenant> DeSerializeNonStandardList()
        {
            var fileDir = Path.Combine(Directory.GetCurrentDirectory(), "Config", "tenancy.json");
            if (!File.Exists(fileDir))
            {
                throw new Exception(string.Format("Ensure tenancy file ({0}) exists or remove this implementation from DI", fileDir));
            }
            String json = File.ReadAllText(fileDir);
            var tenants = JsonConvert.DeserializeObject<IEnumerable<Tenant>>(json);
            return tenants.ToList();
        }
    }
}
