using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Client;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TerraCloudRaspberry.Infrastructure.IoTHub
{
    public class IoTHubService : IIoTHubService
    {
        private const string DeviceConnectionString = "HostName=terracloud.azure-devices.net;DeviceId=Test;SharedAccessKey=+bhjW2qoy/U1dN44tDdotufGSr3SNcg4uAIoTJcoXL4=";
        static DeviceClient deviceClient = null;

        public async Task Start(CancellationToken token)
        {
            try
            {
                Console.WriteLine("Connecting to hub");
                deviceClient = DeviceClient.CreateFromConnectionString(DeviceConnectionString, TransportType.Mqtt);

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
            Console.WriteLine("Oczekiwanie na wiadomości z IoT Hub...");

            while (!token.IsCancellationRequested)
            {
                var receivedMessage = await deviceClient.ReceiveAsync();

                if (receivedMessage != null)
                {
                    var messageData = Encoding.ASCII.GetString(receivedMessage.GetBytes());
                    Console.WriteLine($"Otrzymano wiadomość: {messageData}");

                    await deviceClient.CompleteAsync(receivedMessage);
                }

                await Task.Delay(1000);
            }
        }
    }
}
