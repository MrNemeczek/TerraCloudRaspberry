using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TerraCloudRaspberry.Infrastructure.IoTHub;
using TerraCloudRaspberry.Infrastructure.Relay;
using TerraCloudRaspberry.Infrastructure.Sensor;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb;

namespace TerraCloudRaspberry.Infrastructure
{
    public static class AddDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //IoT Hub
            services.AddScoped<IIoTHubService, IoTHubService>();
            services.Configure<IoTHubOptions>(options => config.GetSection("IoTHub").Bind(options));
            
            //TerraCloud Api
            services.AddScoped<ITerraCloudWebService, TerraCloudWebService>();
            services.Configure<TerraCloudWebOptions>(options => config.GetSection("TerraCloudWeb").Bind(options));

            //Relay
            services.AddScoped<IRelayService, RelayService>();

            //Sensor
            services.AddScoped<ISensorService, SensorService>();

            return services;
        }
    }
}
