namespace TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models.Responses
{
    public class GetDeviceResponse
    {
        public Guid Id { get; set; }
        public string UniqueCode { get; set; }
        public DateTime? LastMeasurement { get; set; }
        public int MeasurementTime { get; set; }
        public int? DayTemperature { get; set; }
        public int? DayHumidity { get; set; }
        public int? NightTemperature { get; set; }
        public int? NightHumidity { get; set; }
    }
}
