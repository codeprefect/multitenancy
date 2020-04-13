using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;
using Multitenant.Services;

namespace Multitenant
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureKestrel(kestrelOptions => {
                    kestrelOptions.ListenAnyIP(5001, listenOptions => {
                        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    });

                    kestrelOptions.ConfigureHttpsDefaults(httpsOptions => {
                        httpsOptions.ServerCertificateSelector = SslCertificateStore.Get;
                    });
                })
                .UseStartup<Startup>();

        private static ICertificateStore SslCertificateStore = new CertificateStore();
    }
}
