using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Threads.HttpClients.Registrars;
using Soenneker.Threads.OpenApiClientUtil.Abstract;

namespace Soenneker.Threads.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class ThreadsOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="ThreadsOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddThreadsOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddThreadsOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IThreadsOpenApiClientUtil, ThreadsOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="ThreadsOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddThreadsOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddThreadsOpenApiHttpClientAsSingleton()
                .TryAddScoped<IThreadsOpenApiClientUtil, ThreadsOpenApiClientUtil>();

        return services;
    }
}
