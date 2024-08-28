using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraCloudRaspberry.Infrastructure.IoTHub;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb;

namespace TerraCloudRaspberry.BackgroundServices
{
    internal class SendMeasurementHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMemoryCache _cache;

        public SendMeasurementHostedService(IServiceProvider serviceProvider, IMemoryCache cache)
        {
            _serviceProvider = serviceProvider;
            _cache = cache;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var terraCloudWebService = scope.ServiceProvider.GetRequiredService<ITerraCloudWebService>();

            await terraCloudWebService.Login();
            var deviceSettings = await terraCloudWebService.GetDeviceSettings();

            while (!stoppingToken.IsCancellationRequested)
            {
                await terraCloudWebService.AddMeasurement();
                await Task.Delay(TimeSpan.FromMinutes(deviceSettings.MeasurementTime));
            }
        }
    }
}
