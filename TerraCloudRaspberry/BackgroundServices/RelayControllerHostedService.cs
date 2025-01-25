using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TerraCloudRaspberry.Infrastructure.Relay;
using TerraCloudRaspberry.Infrastructure.Sensor;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb;

namespace TerraCloudRaspberry.BackgroundServices
{
    internal class RelayControllerHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public RelayControllerHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var terraCloudWebService = scope.ServiceProvider.GetRequiredService<ITerraCloudWebService>();
            var sensorService = scope.ServiceProvider.GetRequiredService<ISensorService>();
            var relayService = scope.ServiceProvider.GetRequiredService<IRelayService>();

            await terraCloudWebService.Login();
            var deviceSettings = await terraCloudWebService.GetDeviceSettings();

            bool test = true;

            while (!stoppingToken.IsCancellationRequested)
            {
                if (test)
                {
                    relayService.TurnOn();
                }
                else
                {
                    relayService.TurnOff();
                }

                test = !test;

                await Task.Delay(TimeSpan.FromMinutes(1));
            }
        }
    }
}
