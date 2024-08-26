using TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models;

namespace TerraCloudRaspberry.Infrastructure.TerraCloudWeb
{
    public interface ITerraCloudWebService
    {
        Task GetDeviceSettings();
        Task AddMeasurement();
        Task<LoginResponse> Login();
    }
}
