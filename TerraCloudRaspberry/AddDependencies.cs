using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TerraCloudRaspberry.BackgroundServices;
using TerraCloudRaspberry.Infrastructure.IoTHub;

namespace TerraCloudRaspberry
{
    public static class AddDependencies
    {
        public static IServiceCollection AddProgram(this IServiceCollection services)
        {
            services.AddHostedService<ReceiveMsgsFromCloudHostedService>();

            return services;
        }
    }
}
