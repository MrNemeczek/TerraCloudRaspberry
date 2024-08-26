using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using TerraCloudRaspberry.BackgroundServices;

namespace TerraCloudRaspberry
{
    public static class AddDependencies
    {
        public static IServiceCollection AddProgram(this IServiceCollection services, IConfiguration config)
        {
            services.AddHostedService<ReceiveMsgsFromCloudHostedService>();
            services.AddHostedService<SendMeasurementHostedService>();

            services.AddHttpClient("terracloud", client =>
            {
                client.BaseAddress = new Uri(config["TerraCloudWeb:Url"] ?? "https://localhost:7291/api/");
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            });

            services.AddMemoryCache();

            return services;
        }
    }
}
