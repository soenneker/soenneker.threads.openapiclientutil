using Soenneker.Threads.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Threads.OpenApiClientUtil.Abstract;

/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IThreadsOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    ValueTask<ThreadsOpenApiClient> Get(CancellationToken cancellationToken = default);
}
