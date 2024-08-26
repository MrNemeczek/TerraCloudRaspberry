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

        public SendMeasurementHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var terraCloudWebService = scope.ServiceProvider.GetRequiredService<ITerraCloudWebService>();

            while (!stoppingToken.IsCancellationRequested)
            {
                await terraCloudWebService.AddMeasurement();
                //wziac delay z ustawien
                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
