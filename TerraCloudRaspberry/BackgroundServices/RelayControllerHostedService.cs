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

            while (!stoppingToken.IsCancellationRequested)
            {
                var results = sensorService.ReadData();

                // Pobierz aktualny czas
                var currentTime = DateTime.Now.TimeOfDay;

                // Definicja zakresów dnia i nocy
                var dayStart = TimeSpan.FromHours(8);  // Dzień zaczyna się o 8:00
                var dayEnd = TimeSpan.FromHours(20);   // Dzień kończy się o 20:00

                // Sprawdź, czy aktualny czas mieści się w zakresie dnia
                bool isDay = currentTime >= dayStart && currentTime < dayEnd;

                if (isDay)
                {
                    if (results.Temperature < deviceSettings.DayTemperature)
                    {
                        relayService.TurnOn(); // Włącz przekaźnik, jeśli temperatura za niska w dzień
                    }
                    else
                    {
                        relayService.TurnOff(); // Wyłącz przekaźnik, jeśli temperatura w normie w dzień
                    }
                }
                else
                {
                    if (results.Temperature < deviceSettings.NightTemperature)
                    {
                        relayService.TurnOn(); // Włącz przekaźnik, jeśli temperatura za niska w nocy
                    }
                    else
                    {
                        relayService.TurnOff(); // Wyłącz przekaźnik, jeśli temperatura w normie w nocy
                    }
                }

                // Opóźnienie 5 minut między kolejnymi odczytami
                await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
            }
        }
    }
}
