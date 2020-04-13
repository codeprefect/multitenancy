using System;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Server.Kestrel.Https;
using Microsoft.Extensions.Caching.Memory;

namespace Multitenant.Services
{
    public interface ICertificateStore
    {
        public X509Certificate2 Add(string host, X509Certificate2 cert);
        public X509Certificate2 Get(ConnectionContext context, string host);
    }

    public class CertificateStore : ICertificateStore
    {
        private readonly MemoryCache _store;

        public CertificateStore()
        {
            _store = new MemoryCache(new MemoryCacheOptions());
        }

        public X509Certificate2 Add(string host, X509Certificate2 cert)
        {
            return _store.Set(host, cert);
        }

        public X509Certificate2 Get(ConnectionContext context, string host)
        {
            X509Certificate2 cert = null;

            try {
                if(!_store.TryGetValue(host, out cert))
                {
                    // TODO: try get from redis or another centralized store for a distributed system
                    // you can use any store that is efficient for your use case
                    cert = CertificateLoader.LoadFromStoreCert(host, "My", StoreLocation.CurrentUser, false);
                    Add(host, cert);
                }
            } catch (Exception ex) {
                // TODO: proper error logging later
            }

            return cert;
        }
    }
}
