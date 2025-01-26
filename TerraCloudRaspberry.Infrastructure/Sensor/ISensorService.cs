using TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models.Requests;

namespace TerraCloudRaspberry.Infrastructure.Sensor
{
    public interface ISensorService
    {
        AddDeviceMeasurementRequest ReadData();
    }
}
