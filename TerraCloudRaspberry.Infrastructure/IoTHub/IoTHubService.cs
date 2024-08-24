using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraCloudRaspberry.Infrastructure.IoTHub
{
    public class IoTHubService : IIoTHubService
    {
        private readonly IoTHubOptions _ioTHubOptions;
        public IoTHubService(IOptions<IoTHubOptions> ioTHubOptions)
        {
            _ioTHubOptions = ioTHubOptions.Value;
        }

        public async Task Start(CancellationToken token)
        {
            try
            {
                await ReceiveMessagesAsync(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error in sample: {0}", ex.Message);
            }
        }

        private async Task ReceiveMessagesAsync(CancellationToken token)
        {
            Console.WriteLine("Connecting to hub");
            DeviceClient deviceClient = DeviceClient.CreateFromConnectionString(_ioTHubOptions.ConnectionString, TransportType.Mqtt);
            
            Console.WriteLine("Wait for Message from IoT Hub...");

            while (!token.IsCancellationRequested)
            {
                var receivedMessage = await deviceClient.ReceiveAsync();

                if (receivedMessage != null)
                {
                    var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    Console.WriteLine($"Message received: {messageData}");

                    await deviceClient.CompleteAsync(receivedMessage);
                }

                await Task.Delay(1000);
            }
        }
    }
}
