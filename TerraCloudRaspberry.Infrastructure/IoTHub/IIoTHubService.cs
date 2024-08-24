namespace TerraCloudRaspberry.Infrastructure.IoTHub
{
    public interface IIoTHubService
    {
        Task Start(CancellationToken token);
    }
}
