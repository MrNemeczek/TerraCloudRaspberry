using System.Diagnostics;
using Microsoft.Extensions.Logging;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models.Requests;

namespace TerraCloudRaspberry.Infrastructure.Sensor
{
    internal class SensorService : ISensorService
    {
        private const int busId = 1;
        private const int address = 0x76;

        private readonly ILogger<SensorService> _logger;

        public SensorService(ILogger<SensorService> logger)
        {
            _logger = logger;
        }

        public AddDeviceMeasurementRequest ReadData()
        {
            try
            {
                var startInfo = new ProcessStartInfo
                {
                    FileName = "python3",
                    Arguments = "Sensor/read_bme280.py",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                using (var process = Process.Start(startInfo))
                {
                    if (process == null)
                    {
                        _logger.LogError("Nie udało się uruchomić procesu.");
                        
                        throw new Exception();
                    }

                    string output = process.StandardOutput.ReadToEnd();
                    process.WaitForExit();

                    var data = output.Trim().Split(',');

                    if (data.Length == 3)
                    {
                        var humidity = double.Parse(data[0]);
                        var pressure = double.Parse(data[1]);
                        var temperature = double.Parse(data[2]);

                        _logger.LogInformation($"Humidity: {humidity:F2} %");
                        _logger.LogInformation($"Pressure: {pressure:F2} hPa");
                        _logger.LogInformation($"Temperature: {temperature:F2} °C");

                        return new AddDeviceMeasurementRequest
                        {
                            Temperature = (int)temperature,
                            Humidity = (int)humidity
                        };
                    }
                    else
                    {
                        _logger.LogInformation("Nieprawidłowe dane z sensora.");
                        
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Wystąpił błąd: {ex.Message}");

                throw ex;
            }
        }
    }
}