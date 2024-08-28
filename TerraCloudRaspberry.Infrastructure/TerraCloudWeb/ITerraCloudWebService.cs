using TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models.Responses;

namespace TerraCloudRaspberry.Infrastructure.TerraCloudWeb
{
    public interface ITerraCloudWebService
    {
        Task<GetDeviceResponse> GetDeviceSettings();
        Task AddMeasurement();
        Task<LoginResponse> Login();
    }
}
