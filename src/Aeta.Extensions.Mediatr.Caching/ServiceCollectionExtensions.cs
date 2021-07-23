using Microsoft.Extensions.DependencyInjection;

namespace Aeta.Extensions.Mediatr.Caching
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInMemoryRequestPipelineCache(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped(typeof(IRequestPipelineCache<>), typeof(InMemoryRequestPipelineCache<>));
            return services;
        }
    }
}