using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraCloudRaspberry.Infrastructure.IoTHub;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb;

namespace TerraCloudRaspberry.Infrastructure
{
    public static class AddDependencies
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            //IoT Hub
            services.AddScoped<IIoTHubService, IoTHubService>();
            services.Configure<IoTHubOptions>(options => config.GetSection("IoTHub"));
            
            //TerraCloud Api
            services.AddScoped<ITerraCloudWebService, TerraCloudWebService>();
            services.Configure<TerraCloudWebOptions>(options => config.GetSection("TerraCloudWeb"));

            return services;
        }
    }
}
