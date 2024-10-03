
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

        public static IServiceCollection AddRestService(this IServiceCollection services, Func<RestConfig, RestConfig> config)
        {
            RestConfig restConfig = new();
            restConfig = config(restConfig);

            return services.AddRestService(restConfig);
        }

        public static IServiceCollection AddRestService(this IServiceCollection services, RestConfig config)
        {
            var restClient = new RestClient(config.BaseUrl);


            foreach (var handle in config.OnConnectionError)
                restClient.ConnectionError += handle;

            foreach (var handle in config.OnServerError)
                restClient.ServerError += handle;

            foreach (var handle in config.OnUnauthorized)
                restClient.Unauthorized += handle;

            foreach (var handle in config.OnForbidden)
                restClient.Forbidden += handle;

            if (config.RetryConnection.HasValue)
                restClient.RetryConection = config.RetryConnection.Value;

            if (config.TimeRetryConnection.HasValue)
                restClient.TimeRetryConnection = config.TimeRetryConnection.Value;

            services.AddScoped(sp => restClient);

            services.TryAdd(ServiceDescriptor.Scoped(typeof(RestService<>), typeof(RestService<>)));

            return services;
        }
    }
}
