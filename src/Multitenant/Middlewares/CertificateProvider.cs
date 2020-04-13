using System.Security.Cryptography.X509Certificates;

namespace Multitenant.Middlewares
{
    public class CertificateProvider : ICertificateProvider
    {
        public X509Certificate GetCertificate(string hostname)
        {
            throw new System.NotImplementedException();
        }
    }
}
