
using BlackDigital.Rest;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BlackDigital.Blazor.Rest
{
    public static class RestHelper
    {
        public static IServiceCollection AddRestService(this IServiceCollection services)
        {
            var restConfig = new RestConfig
            {
                BaseUrl = "https://localhost:5001"
            };
            return services.AddRestService(restConfig);
        }

        public static IServiceCollection AddRestService(this IServiceCollection services, Func<RestConfig, RestConfig> confg)
        {
            RestConfig restConfig = new();
            restConfig = confg(restConfig);

            return services.AddRestService(restConfig);
        }

        public static IServiceCollection AddRestService(this IServiceCollection services, RestConfig config)
        {
            var restClient = new RestClient(config.BaseUrl);

            services.AddScoped(sp => restClient);

            services.TryAdd(ServiceDescriptor.Scoped(typeof(RestService<>), typeof(RestService<>)));

            return services;
        }
    }
}
