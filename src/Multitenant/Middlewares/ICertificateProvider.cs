using System.Security.Cryptography.X509Certificates;

namespace Multitenant.Middlewares
{
    public interface ICertificateProvider
    {
        X509Certificate GetCertificate(string hostname);
    }
}
