using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TerraCloudRaspberry.Persistance.Cache;

namespace TerraCloudRaspberry.Common.Api
{
    internal class ApiRequest : IApiRequest
    {
        private readonly JsonSerializerOptions _options;
        private readonly HttpClient _http;
        private readonly IMemoryCache _cache;

        public ApiRequest(IHttpClientFactory httpClientFactory, IMemoryCache cache)
        {
            _options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };

            _http = httpClientFactory.CreateClient("terracloud");
            _cache = cache;
        }

        public async Task<TResult> PostAsync<TResult, TBody>(string endpoint, TBody body)
        {
            try
            {
                await SetAuthorization();

                string requestBody = JsonSerializer.Serialize(body);
                var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _http.PostAsync(endpoint, httpContent);
                response.EnsureSuccessStatusCode();

                var result = await DeserializeResponse<TResult>(response);

                return result;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());

                throw;
            }
        }
        public async Task<ErrorResponse?> OnlyPostAsync<TBody>(string endpoint, TBody body)
        {
            try
            {
                await SetAuthorization();

                string requestBody = JsonSerializer.Serialize(body);
                var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _http.PostAsync(endpoint, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await DeserializeResponse<ErrorResponse>(response);

                    return errorResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());

                throw;
            }
        }
        public async Task<ErrorResponse?> OnlyPatchAsync<TBody>(string endpoint, TBody body)
        {
            try
            {
                await SetAuthorization();

                string requestBody = JsonSerializer.Serialize(body);
                var httpContent = new StringContent(requestBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _http.PatchAsync(endpoint, httpContent);
                if (!response.IsSuccessStatusCode)
                {
                    var errorResponse = await DeserializeResponse<ErrorResponse>(response);

                    return errorResponse;
                }

                return null;
            }
            catch (Exception ex)
            {
                await Console.Out.WriteLineAsync(ex.ToString());

                throw;
            }
        }
        public async Task<TResult> GetAsync<TResult>(string endpoint)
        {
            await SetAuthorization();

            HttpResponseMessage response = await _http.GetAsync(endpoint);
            response.EnsureSuccessStatusCode();

            var result = await DeserializeResponse<TResult>(response);

            return result;
        }
        public async Task<TResult> PutAsync<TResult>(string endpoint)
        {
            await SetAuthorization();

            throw new NotImplementedException();
        }
        public async Task<ErrorResponse?> DeleteAsync(string endpoint)
        {
            await SetAuthorization();

            HttpResponseMessage response = await _http.DeleteAsync(endpoint);
            if (!response.IsSuccessStatusCode)
            {
                var errorResponse = await DeserializeResponse<ErrorResponse>(response);

                return errorResponse;
            }

            return null;
        }

        private async Task<TResult> DeserializeResponse<TResult>(HttpResponseMessage response)
        {
            string responseBody = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<TResult>(responseBody, _options);

            return result;
        }

        private async Task SetAuthorization()
        {
            if (!_cache.TryGetValue(CacheKeys.JWT, out string? tokenJWT))
            {
                return;
            }

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenJWT);
        }
    }
}
