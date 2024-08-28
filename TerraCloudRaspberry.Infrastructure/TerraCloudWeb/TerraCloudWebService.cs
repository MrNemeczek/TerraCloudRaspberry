using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TerraCloudRaspberry.Common.Api;
using TerraCloudRaspberry.Infrastructure.IoTHub;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models;
using TerraCloudRaspberry.Infrastructure.TerraCloudWeb.Models.Responses;
using TerraCloudRaspberry.Persistance.Cache;

namespace TerraCloudRaspberry.Infrastructure.TerraCloudWeb
{
    public class TerraCloudWebService : ITerraCloudWebService
    {
        private readonly IApiRequest _apiRequest;
        private readonly IMemoryCache _cache;
        private readonly IoTHubOptions _ioTHubOptions;
        private readonly TerraCloudWebOptions _terraCloudWebOptions;


        public TerraCloudWebService(IApiRequest apiRequest, IOptions<IoTHubOptions> ioTHubOptions, IOptions<TerraCloudWebOptions> terraCloudWebOptions, IMemoryCache cache)
        {
            _apiRequest = apiRequest;
            _ioTHubOptions = ioTHubOptions.Value;
            _terraCloudWebOptions = terraCloudWebOptions.Value;
            _cache = cache;
        }

        public async Task AddMeasurement()
        {
            if (!_cache.TryGetValue(CacheKeys.JWT, out string? tokenJWT))
            {
                await Login();
            }

            // TODO: zczytywanie danych z czujnikow
            AddDeviceMeasurementRequest request = new AddDeviceMeasurementRequest()
            {
                UniqueCode = _ioTHubOptions.DeviceUniqueCode,
                Temperature = 22,
                Humidity = 33
            };

            await _apiRequest.OnlyPostAsync<AddDeviceMeasurementRequest>(Endpoints.AddMeasurement, request);
        }

        public async Task<GetDeviceResponse> GetDeviceSettings()
        {
            GetDeviceResponse response = await _apiRequest.GetAsync<GetDeviceResponse>(Endpoints.GetDevice + _terraCloudWebOptions.DeviceId);
            _cache.Set(CacheKeys.Device, response);

            return response;
        }

        public async Task<LoginResponse> Login()
        {            
            LoginRequest loginRequest = new LoginRequest()
            {
                Id = _terraCloudWebOptions.DeviceId,
                UniqueCode = _ioTHubOptions.DeviceUniqueCode
            };

            LoginResponse response = await _apiRequest.PostAsync<LoginResponse, LoginRequest>(Endpoints.Auth, loginRequest);
            _cache.Set(CacheKeys.JWT, response.Token);

            return response;
        }
    }
}
