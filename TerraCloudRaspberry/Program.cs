using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

using TerraCloudRaspberry.Common;
using TerraCloudRaspberry.Infrastructure;

namespace TerraCloudRaspberry
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
            IHostEnvironment hostingEnvironment = builder.Environment;
            builder.Configuration
                    .SetBasePath(hostingEnvironment.ContentRootPath)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            IConfiguration config = builder.Configuration;

            builder.Services.AddCommon()
                .AddInfrastructure(config)
                .AddProgram();

            using IHost host = builder.Build();
            await host.RunAsync();
        }
    }
}
