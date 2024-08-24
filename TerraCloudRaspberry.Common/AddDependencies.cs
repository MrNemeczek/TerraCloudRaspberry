using Microsoft.Extensions.DependencyInjection;

using TerraCloudRaspberry.Common.Api;

namespace TerraCloudRaspberry.Common
{
    public static class AddDependencies
    {
        public static IServiceCollection AddCommon(this IServiceCollection services)
        {
            services.AddScoped<IApiRequest, ApiRequest>();

            return services;
        }
    }
}
