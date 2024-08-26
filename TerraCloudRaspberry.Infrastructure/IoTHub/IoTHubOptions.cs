namespace TerraCloudRaspberry.Infrastructure.IoTHub
{
    public class IoTHubOptions
    {
        public string ConnectionString { get; set; } = null!;
        public string DeviceUniqueCode { get; set; } = null!;
    }
}
