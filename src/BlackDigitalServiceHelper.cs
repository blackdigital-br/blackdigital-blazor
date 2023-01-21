
using BlackDigital.Rest;
using Microsoft.Extensions.DependencyInjection;

namespace BlackDigital.Blazor
{
    public static class BlackDigitalServiceHelper
    {
        public static IServiceCollection AddRestService(this IServiceCollection services, 
                                                Func<RestServiceBuilder, RestServiceBuilder> restService)
        {
            RestServiceBuilder restServiceBuilder = new();
            restServiceBuilder = restService(restServiceBuilder);

            var types = restServiceBuilder.Build();

            foreach (var typePair in types)
                services.AddScoped(typePair.Key, typePair.Value);

            return services;
        }

    }
}
