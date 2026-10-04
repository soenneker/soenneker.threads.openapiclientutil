using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Threads.HttpClients.Abstract;
using Soenneker.Threads.OpenApiClientUtil.Abstract;
using Soenneker.Threads.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Threads.OpenApiClientUtil;

public sealed class ThreadsOpenApiClientUtil : IThreadsOpenApiClientUtil
{
    private readonly AsyncSingleton<ThreadsOpenApiClient> _client;

    public ThreadsOpenApiClientUtil(IThreadsOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<ThreadsOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("Threads:AccessToken");
            string authHeaderName = configuration["Threads:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Threads:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

            string? baseUrl = configuration["Threads:ClientBaseUrl"];
            if (!string.IsNullOrWhiteSpace(baseUrl)) requestAdapter.BaseUrl = baseUrl;
            return new ThreadsOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<ThreadsOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
