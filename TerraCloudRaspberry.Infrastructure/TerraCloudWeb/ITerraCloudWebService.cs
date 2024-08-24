namespace TerraCloudRaspberry.Infrastructure.TerraCloudWeb
{
    public interface ITerraCloudWebService
    {
        Task GetDeviceSettings();
        Task AddMeasurement();
    }
}
