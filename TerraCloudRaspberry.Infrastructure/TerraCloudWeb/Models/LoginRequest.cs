namespace TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models
{
    public class LoginRequest
    {
        public string UniqueCode { get; set; }
        /// <summary>
        /// Device ID
        /// </summary>
        public Guid Id { get; set; }
    }
}
