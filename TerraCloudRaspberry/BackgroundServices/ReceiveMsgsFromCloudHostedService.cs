using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using TerraCloudRaspberry.Infrastructure.IoTHub;

namespace TerraCloudRaspberry.BackgroundServices
{
    internal class ReceiveMsgsFromCloudHostedService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

        public ReceiveMsgsFromCloudHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var ioTHubService = scope.ServiceProvider.GetRequiredService<IIoTHubService>();

            await ioTHubService.Start(stoppingToken);
        }
    }
}
