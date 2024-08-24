using System.Net;

namespace TerraCloudRaspberry.Common.Api
{
    public class ErrorResponse
    {
        public string Describe { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
